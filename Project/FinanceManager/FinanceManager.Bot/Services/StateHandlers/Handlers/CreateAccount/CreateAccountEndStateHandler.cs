using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class CreateAccountEndStateHandler : CompleteStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;
    private readonly ICategoriesInitializer _categoriesInitializer;

    public CreateAccountEndStateHandler(
        IAccountManager accountManager,
        ITransactionManager transactionManager,
        ICategoriesInitializer categoriesInitializer,
        IMessageSenderManager messageSender)
        : base(messageSender)
    {
        _accountManager = accountManager;
        _transactionManager = transactionManager;
        _categoriesInitializer = categoriesInitializer;
    }

    private protected override async Task HandleCompleteAsync(BotUpdateContext updateContext)
    {
        await CreateAccount(updateContext);
        await _categoriesInitializer.InitializeDefaults(updateContext.Session.Id, updateContext.CancellationToken);
    }

    private async Task CreateAccount(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetCreateAccountContext();
        ArgumentNullException.ThrowIfNull(context.Currency, nameof(context.Currency));

        var defaultAccount = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        var isDefaultExists = defaultAccount is not null;

        var accountCommand = new CreateAccountDto
        {
            UserId = updateContext.Session.Id,
            CurrencyId = context.Currency.Id,
            Title = context.AccountName,
            IsDefault = !isDefaultExists,
            IsArchived = false
        };

        var account = await _accountManager.Create(accountCommand, updateContext.CancellationToken);

        var command = new CreateTransactionDto
        {
            UserId = updateContext.Session.Id,
            AccountId = account.Id,
            Amount = context.InitialBalance,
            CategoryId = null,
            Date = DateOnly.FromDateTime(DateTime.Now),
            TransactionType = TransactionType.Income,
            Description = "Initial balance"
        };

        var transaction = await _transactionManager.Create(command, updateContext.CancellationToken);

        var message = account.Currency is null ?
            $"The account {account.Title} with initial balance {context.InitialBalance} has been created!" :
            $"The account {account.Title} with initial balance {context.InitialBalance} {account.Currency.CurrencyCode} " +
            $"{account.Currency.Emoji} has been created!";

        await _messageSender.SendApproveMessage(updateContext, message);
    }
}
