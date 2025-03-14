using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Core.Enums;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

public abstract class BaseChooseCategoryTypeStateHandler : IStateHandler
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    protected BaseChooseCategoryTypeStateHandler(
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _callbackDataProvider = callbackDataProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackDataAsync(updateContext, true);

        await _messageManager.DeleteLastMessageAsync(updateContext);

        if (callbackData is null || string.IsNullOrWhiteSpace(callbackData.Data))
            return await _sessionStateManager.Previous(updateContext.Session);

        var transactionTypeText = callbackData.Data;

        CategoryType categoryType;
        if (transactionTypeText == CategoryType.Expense.GetDescription())
            categoryType = CategoryType.Expense;
        else if (transactionTypeText == TransactionType.Income.GetDescription())
            categoryType = CategoryType.Income;
        else
        {
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        SaveCategoryToContext(updateContext.Session, categoryType);
        return await _sessionStateManager.Next(updateContext.Session);
    }

    protected abstract void SaveCategoryToContext(UserSession session, CategoryType categoryType);
}
