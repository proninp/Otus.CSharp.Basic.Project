using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class CreateAccountEndStateHandler : CompleteSubStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;
    private readonly ICategoriesInitializer _categoriesInitializer;

    public CreateAccountEndStateHandler(
        IAccountManager accountManager,
        ITransactionManager transactionManager,
        ICategoriesInitializer categoriesInitializer,
        IChatProvider chatProvider,
        IMessageSenderManager messageSender)
        : base(chatProvider, messageSender)
    {
        _accountManager = accountManager;
        _transactionManager = transactionManager;
        _categoriesInitializer = categoriesInitializer;
    }

    private protected override async Task HandleCompleteAsync(
        UserSession session, ITelegramBotClient botClient, Update update, Chat chat, CancellationToken cancellationToken)
    {
        await CreateAccount(session, botClient, update, chat, cancellationToken);
        await _categoriesInitializer.InitializeDefaults(session.Id, cancellationToken);
    }

    private async Task CreateAccount(
        UserSession session, ITelegramBotClient botClient, Update update, Chat chat, CancellationToken cancellationToken)
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

        await _messageSender.SendApproveMessage(botClient, chat, message, cancellationToken);
    }
}
