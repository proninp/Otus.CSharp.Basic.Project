using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class CreateAccountStateHandler : IStateHandler
{
    private readonly ISubStateHandlerFactory _subStateHandlerFactory;
    private readonly IAccountManager _accountManager;
    private readonly IChatProvider _chatProvider;

    public CreateAccountStateHandler(
        ISubStateHandlerFactory subStateHandlerFactory,
        IAccountManager accountManager,
        IChatProvider chatProvider)
    {
        _subStateHandlerFactory = subStateHandlerFactory;
        _accountManager = accountManager;
        _chatProvider = chatProvider;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var subStateHandler = _subStateHandlerFactory.GetSubStateHandler(userSession.SubState);
        userSession.SubState = await subStateHandler.HandleAsync(userSession, botClient, update, cancellationToken);

        if (userSession.SubState == UserSubState.SendCurrencies)
            return await HandleStateAsync(userSession, botClient, update, cancellationToken);

        if (userSession.SubState == UserSubState.Complete)
        {
            await CreateAccount(userSession, botClient, update, cancellationToken);
            userSession.ResetState();
        }
        return userSession.UserState;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

        var chat = _chatProvider.GetChat(update);
        await botClient.SendMessage(
                chat, $"{Enums.Emoji.Success.GetSymbol()} " +
                $"The account {account.Title} with initial balance {context.InitialBalance} has been created.",
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}