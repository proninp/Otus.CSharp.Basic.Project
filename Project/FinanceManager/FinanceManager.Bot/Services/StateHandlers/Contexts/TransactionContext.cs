using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public sealed class TransactionContext
{
    public TransactionType TransactionType { get; init; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public CategoryDto? Category { get; set; }

    public string TransactionTypeDescription => TransactionType.GetDescription().ToLower();

    public static TransactionContext CreateExpenseContext() =>
        new TransactionContext { TransactionType = TransactionType.Expense };

    public static TransactionContext CreateIncomeContext() =>
        new TransactionContext { TransactionType = TransactionType.Income };
}

public static class TransactionContextExtesion
{
    public static TransactionContext GetTransactionContext(this UserSession session)
    {
        if (session.WorkflowContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext));
        if (session.WorkflowContext.TransactionContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext.TransactionContext));
        return session.WorkflowContext.TransactionContext;
    }
}