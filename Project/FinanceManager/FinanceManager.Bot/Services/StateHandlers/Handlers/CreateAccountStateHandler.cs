using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers;
public class CreateAccountStateHandler : IStateHandler
{
    private readonly ISubStateFactoryProvider _subStateFactoryProvider;
    private readonly IAccountManager _accountManager;
    private readonly ICategoriesInitializer _categoriesInitializer;
    private readonly IChatProvider _chatProvider;

    public CreateAccountStateHandler(
        ISubStateFactoryProvider subStateFactoryProvider,
        IAccountManager accountManager,
        IChatProvider chatProvider,
        ICategoriesInitializer categoriesInitializer)
    {
        _subStateFactoryProvider = subStateFactoryProvider;
        _accountManager = accountManager;
        _chatProvider = chatProvider;
        _categoriesInitializer = categoriesInitializer;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var subStateHandlerFactory = _subStateFactoryProvider.GetSubStateFactory(userSession.UserState);
        var subStateHandler = subStateHandlerFactory.GetSubStateHandler(userSession.SubState);

        userSession.SubState = await subStateHandler.HandleAsync(userSession, botClient, update, cancellationToken);

        if (userSession.SubState == UserSubState.SendCurrencies)
            return await HandleStateAsync(userSession, botClient, update, cancellationToken);

        if (userSession.SubState == UserSubState.Complete)
            await Complete(userSession, botClient, update, cancellationToken);

        return userSession.UserState;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task Complete(UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await CreateAccount(userSession, botClient, update, cancellationToken);
        await _categoriesInitializer.InitializeDefaults(userSession.Id, cancellationToken);
        userSession.ResetState();
    }

    private async Task CreateAccount(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var context = userSession.GetCreateAccountContext();
        ArgumentNullException.ThrowIfNull(context.Currency, nameof(context.Currency));

        var defaultAccount = await _accountManager.GetDefault(userSession.Id, cancellationToken);
        var isDefaultExists = defaultAccount is not null;

        var command = new CreateAccountDto
        {
            UserId = userSession.Id,
            CurrencyId = context.Currency.Id,
            Title = context.AccountName,
            IsDefault = !isDefaultExists,
            IsArchived = false
        };
        var account = await _accountManager.Create(command, cancellationToken);

        var message = account.Currency is null ?
            $"The account {account.Title} with initial balance {context.InitialBalance} has been created!" :
            $"The account {account.Title} with initial balance {context.InitialBalance} {account.Currency.CurrencyCode} " +
            $"{account.Currency.Emoji} has been created!";

        var chat = _chatProvider.GetChat(update);
        await botClient.SendMessage(
                chat, $"{Enums.Emoji.Success.GetSymbol()} " +
                message,
                parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}