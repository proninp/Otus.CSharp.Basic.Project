using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class TransactionComplitionStateHandler : CompleteStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;

    public TransactionComplitionStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IAccountManager accountManager,
        ITransactionManager transactionManager)
        : base(messageManager, sessionStateManager)
    {
        _accountManager = accountManager;
        _transactionManager = transactionManager;
    }

    private protected override async Task HandleCompleteAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();
        if (context.Amount <= 0)
        {
            await _messageManager.SendErrorMessageAsync(updateContext,
                "The transaction was not registered because a zero amount was entered.");
            return;
        }

        var account = await _accountManager.GetDefaultAsync(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            await _messageManager.SendErrorMessageAsync(updateContext,
                "The operation cannot be performed because you do not have a default account.");
            return;
        }

        var command = new CreateTransactionDto
        {
            UserId = updateContext.Session.Id,
            AccountId = account.Id,
            Amount = context.Amount,
            CategoryId = context.Category?.Id,
            Date = context.Date,
            TransactionType = context.TransactionType
        };

        var transaction = await _transactionManager.CreateAsync(command, updateContext.CancellationToken);

        var message = $"The {command.Amount}";
        if (account.Currency is not null)
            message += $" {account.Currency.CurrencyCode} {account.Currency.Emoji}";
        message += " transaction was registered successfully";

        await _messageManager.SendApproveMessageAsync(updateContext, message);
    }
}