using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
public abstract class BaseChooseCategoryStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    protected BaseChooseCategoryStateHandler(
        ICategoryManager categoryManager,
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _categoryManager = categoryManager;
        _callbackDataProvider = callbackDataProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackDataAsync(updateContext, true);

        if (callbackData is null || string.IsNullOrEmpty(callbackData.Data))
        {
            await _messageManager.DeleteLastMessageAsync(updateContext);
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        var categoryId = callbackData.Data;

        CategoryDto? category = null;

        if (categoryId != Guid.Empty.ToString())
        {
            category = await _categoryManager.GetByIdAsync(new Guid(categoryId), updateContext.CancellationToken);
            if (category is null)
                return await _sessionStateManager.Previous(updateContext.Session);
        }

        SaveCategoryToContext(updateContext.Session, category);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    protected abstract void SaveCategoryToContext(UserSession session, CategoryDto? category);
}