using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionRegistrationStateHandler : CompleteStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;

    public TransactionRegistrationStateHandler(
        IMessageManager messageManager,
        IAccountManager accountManager,
        ITransactionManager transactionManager)
        : base(messageManager)
    {
        _accountManager = accountManager;
        _transactionManager = transactionManager;
    }

    private protected override async Task HandleCompleteAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();
        if (context.Amount <= 0)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The transaction was not registered because a zero amount was entered.");
            return;
        }

        var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            await _messageManager.SendErrorMessage(updateContext,
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

        var transaction = await _transactionManager.Create(command, updateContext.CancellationToken);
        var balance = await _accountManager.GetBalance(account, updateContext.CancellationToken);

        var message = $"The transaction was successfully registered{Environment.NewLine}" +
            $"Current balance: {balance}";
        if (account.Currency is not null)
            message += $" {account.Currency.CurrencyCode} {account.Currency.Emoji}";

        await _messageManager.SendApproveMessage(updateContext, message);
    }
}