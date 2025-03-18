using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;

public sealed class RenameCategoryContext
{
    public CategoryType CategoryType { get; set; }

    public CategoryDto? Category { get; set; }

    public string? CategoryName { get; set; }

    public string Emoji { get; set; } = string.Empty;
}

public static class RenameCategoryContextExtesion
{
    public static RenameCategoryContext GetRenameCategoryContext(this UserSession session)
    {
        if (session.WorkflowContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext));
        if (session.WorkflowContext.RenameCategoryContext is null)
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext.RenameCategoryContext));
        return session.WorkflowContext.RenameCategoryContext;
    }
}