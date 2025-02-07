using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public sealed class HistoryContext
{
    public AccountDto AccountDto { get; init; }

    public long TransactionsCount { get; init; }
    
    public int PageIndex { get; set; } = 0;

    public int PageSize { get; } = 12;

    public HistoryContext(AccountDto accountDto, long transactionsCount)
    {
        AccountDto = accountDto;
        TransactionsCount = transactionsCount;
    }
}

public static class HistoryContextExtesion
{
    public static HistoryContext GetHistoryContext(this UserSession session)
    {
        if (session.WorkflowContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext));
        if (session.WorkflowContext.HistoryContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext.HistoryContext));
        return session.WorkflowContext.HistoryContext;
    }
}