using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;

public sealed class DeleteCategoryContext
{
    public TransactionType CategoryType { get; set; }

    public CategoryDto? Category { get; set; }
}

public static class DeleteCategoryContextExtesion
{
    public static DeleteCategoryContext GetDeleteCategoryContext(this UserSession session)
    {
        if (session.WorkflowContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext));
        if (session.WorkflowContext.DeleteCategoryContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext.DeleteCategoryContext));
        return session.WorkflowContext.DeleteCategoryContext;
    }
}