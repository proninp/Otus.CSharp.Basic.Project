using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Services.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers;
public class CreateAccountStateHandler : IStateHandler
{
    private readonly ISubStateFactoryProvider _subStateFactoryProvider;
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;
    private readonly ICategoriesInitializer _categoriesInitializer;
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public CreateAccountStateHandler(
        ISubStateFactoryProvider subStateFactoryProvider,
        IAccountManager accountManager,
        ITransactionManager transactionManager,
        IChatProvider chatProvider,
        ICategoriesInitializer categoriesInitializer,
        IMessageSenderManager messageSender)
    {
        _subStateFactoryProvider = subStateFactoryProvider;
        _accountManager = accountManager;
        _transactionManager = transactionManager;
        _chatProvider = chatProvider;
        _categoriesInitializer = categoriesInitializer;
        _messageSender = messageSender;
    }

    public async Task HandleStateAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var subStateHandlerFactory = _subStateFactoryProvider.GetSubStateFactory(session.State);
        var subStateHandler = subStateHandlerFactory.GetSubStateHandler(session.SubState);

        await subStateHandler.HandleAsync(session, botClient, update, cancellationToken);

        if (session.SubState == WorkflowSubState.Complete)
            await Complete(session, botClient, update, cancellationToken);
    }

    public Task RollBackAsync(UserSession session, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task Complete(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await CreateAccount(session, botClient, update, cancellationToken);
        await _categoriesInitializer.InitializeDefaults(session.Id, cancellationToken);
        session.ResetState();
    }

    private async Task CreateAccount(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var context = session.GetCreateAccountContext();
        ArgumentNullException.ThrowIfNull(context.Currency, nameof(context.Currency));

        var defaultAccount = await _accountManager.GetDefault(session.Id, cancellationToken);
        var isDefaultExists = defaultAccount is not null;

        var accountCommand = new CreateAccountDto
        {
            UserId = session.Id,
            CurrencyId = context.Currency.Id,
            Title = context.AccountName,
            IsDefault = !isDefaultExists,
            IsArchived = false
        };
        var account = await _accountManager.Create(accountCommand, cancellationToken);

        var command = new CreateTransactionDto
        {
            UserId = session.Id,
            AccountId = account.Id,
            Amount = context.InitialBalance,
            CategoryId = null,
            Date = DateOnly.FromDateTime(DateTime.Now),
            TransactionType = TransactionType.Income,
            Description = "Initial balance"
        };

        var transaction = await _transactionManager.Create(command, cancellationToken);

        var message = account.Currency is null ?
            $"The account {account.Title} with initial balance {context.InitialBalance} has been created!" :
            $"The account {account.Title} with initial balance {context.InitialBalance} {account.Currency.CurrencyCode} " +
            $"{account.Currency.Emoji} has been created!";

        var chat = _chatProvider.GetChat(update);
        await _messageSender.SendApproveMessage(botClient, chat, message, cancellationToken);
    }
}