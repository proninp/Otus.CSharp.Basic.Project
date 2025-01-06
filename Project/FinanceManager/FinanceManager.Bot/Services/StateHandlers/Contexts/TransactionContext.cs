using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.CommandHandlers.Contexts;
public class TransactionContext
{
    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public CategoryDto? Category { get; set; }
}

public static class TransactionContextExtesion
{
    public static TransactionContext GetTransactionContext(this UserSession session)
    {
        TransactionContext? transactionContext;
        if (session.ContextData is null)
            throw new ArgumentNullException(nameof(transactionContext));
        transactionContext = session.ContextData as TransactionContext;
        if (transactionContext is null)
            throw new InvalidCastException(nameof(transactionContext));
        return transactionContext;
    }
}