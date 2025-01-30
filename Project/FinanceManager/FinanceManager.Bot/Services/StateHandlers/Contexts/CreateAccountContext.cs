using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public class CreateAccountContext
{
    public string? AccountName { get; set; }

    public CurrencyDto? Currency { get; set; }

    public decimal InitialBalance { get; set; }
}

public static class CreateAccountContextExtesion
{
    public static CreateAccountContext GetCreateAccountContext(this UserSession session)
    {
        if (session.WorkflowContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext));
        if (session.WorkflowContext.CreateAccountContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext.CreateAccountContext));
        return session.WorkflowContext.CreateAccountContext;
    }
}