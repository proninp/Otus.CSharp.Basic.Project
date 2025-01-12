using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

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

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var previousState = WorkflowState.SendTransactionCategories;
        if (!_callbackQueryProvider.GetCallbackQuery(update, out var callbackQuery))
        {
            session.Continue(previousState);
            return;
        }

        var categoryId = callbackQuery.Data;

        if (string.IsNullOrEmpty(categoryId))
        {
            session.Continue(previousState);
            return;
        }

        CategoryDto? category = null;

        if (categoryId != Guid.Empty.ToString())
        {
            category = await _categoryManager.GetById(new Guid(categoryId), cancellationToken);
            if (category is null)
            {
                session.Continue(previousState);
                return;
            }
        }

        var context = session.GetTransactionContext();
        context.Category = category;

        session.Continue(WorkflowState.SendTransactionDateSelection);
    }
}
