using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.CommandHandlers.Contexts;
public class TransactionContext
{
    public TransactionType TransactionType { get; init; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public CategoryDto? Category { get; set; }

    public string TransactionTypeDescription => TransactionType.GetDescription().ToLower();
}

public static class TransactionContextExtesion
{
    public static TransactionContext GetTransactionContext(this UserSession session)
    {
        TransactionContext? transactionContext;
        if (session.WorkflowContext is null)
            throw new ArgumentNullException(nameof(transactionContext));
        transactionContext = session.WorkflowContext as TransactionContext;
        if (transactionContext is null)
            throw new InvalidCastException(nameof(transactionContext));
        return transactionContext;
    }
}