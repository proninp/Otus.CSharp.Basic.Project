using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class ChooseCategoryStateHandler : IStateHandler
{
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly ICategoryManager _categoryManager;

    public ChooseCategoryStateHandler(
        IUpdateCallbackQueryProvider callbackQueryProvider,
        ICategoryManager categoryManager)
    {
        _callbackQueryProvider = callbackQueryProvider;
        _categoryManager = categoryManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.SendTransactionCategories;
        if (!_callbackQueryProvider.GetCallbackQuery(updateContext.Update, out var callbackQuery))
        {
            updateContext.Session.Continue(previousState);
            return;
        }

        var categoryId = callbackQuery.Data;

        if (string.IsNullOrEmpty(categoryId))
        {
            updateContext.Session.Continue(previousState);
            return;
        }

        CategoryDto? category = null;

        if (categoryId != Guid.Empty.ToString())
        {
            category = await _categoryManager.GetById(new Guid(categoryId), updateContext.CancellationToken);
            if (category is null)
            {
                updateContext.Session.Continue(previousState);
                return;
            }
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Category = category;

        updateContext.Session.Continue(WorkflowState.SendTransactionDateSelection);
    }
}