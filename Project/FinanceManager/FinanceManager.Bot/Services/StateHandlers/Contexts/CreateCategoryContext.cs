using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Contexts;
public sealed class CreateCategoryContext
{
    public CategoryType CategoryType { get; set; }

    public string? CategoryTitle { get; set; }

    public string Emoji { get; set; } = string.Empty;

    public static CreateCategoryContext CreateContext(CategoryType type) =>
        new CreateCategoryContext { CategoryType = type };
}

public static class CreateCategoryContextExtesion
{
    public static CreateCategoryContext GetCreateCategoryContext(this UserSession session)
    {
        if (session.WorkflowContext is null)
        {
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext));
        }

        if (session.WorkflowContext.CreateCategoryContext is null)
        {
            throw new StateContextNullException(session.State, nameof(session.WorkflowContext.CreateCategoryContext));
        }
        return session.WorkflowContext.CreateCategoryContext;
    }
}