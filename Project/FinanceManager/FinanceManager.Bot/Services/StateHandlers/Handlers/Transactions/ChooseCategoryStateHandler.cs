using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class ChooseCategoryStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public ChooseCategoryStateHandler(
        ICallbackDataProvider callbackQueryProvider,
        ICategoryManager categoryManager,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _callbackDataProvider = callbackQueryProvider;
        _categoryManager = categoryManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true);
        if (callbackData is null)
        {
            _sessionStateManager.Previous(updateContext.Session);
            return;
        }

        var categoryId = callbackData.Data;

        if (string.IsNullOrEmpty(categoryId))
        {
            await _messageManager.DeleteLastMessage(updateContext);
            _sessionStateManager.Previous(updateContext.Session);
            return;
        }

        CategoryDto? category = null;

        if (categoryId != Guid.Empty.ToString())
        {
            category = await _categoryManager.GetById(new Guid(categoryId), updateContext.CancellationToken);
            if (category is null)
            {
                _sessionStateManager.Previous(updateContext.Session);
                return;
            }
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Category = category;

        _sessionStateManager.Next(updateContext.Session);
    }
}