using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public class HistoryContext
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
        HistoryContext? historyContext;
        if (session.WorkflowContext is null)
            throw new StateContextNullException(session.State, nameof(historyContext));
        historyContext = session.WorkflowContext as HistoryContext;
        if (historyContext is null)
            throw new InvalidCastException(nameof(historyContext));
        return historyContext;
    }
}