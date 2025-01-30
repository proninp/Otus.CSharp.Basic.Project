using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class ChooseCategoryStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;
    private readonly IMessageManager _messageManager;
    private readonly IUserSessionStateManager _sessionStateManager;

    public ChooseCategoryStateHandler(
        ICallbackDataProvider callbackQueryProvider,
        ICategoryManager categoryManager,
        ICallbackDataValidator callbackDataValidator,
        IMessageManager messageManager,
        IUserSessionStateManager sessionStateManager)
    {
        _callbackDataProvider = callbackQueryProvider;
        _categoryManager = categoryManager;
        _callbackDataValidator = callbackDataValidator;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.SendTransactionCategories;
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true, previousState);
        if (callbackData is null)
            return;

        //if (!await _callbackDataValidator.Validate(updateContext, callbackData))
        //{
        //    _sessionStateManager.Wait(updateContext.Session);
        //    return;
        //}

        var categoryId = callbackData.Data;

        if (string.IsNullOrEmpty(categoryId))
        {
            await _messageManager.DeleteLastMessage(updateContext);
            _sessionStateManager.Continue(updateContext.Session, previousState);
            return;
        }

        CategoryDto? category = null;

        if (categoryId != Guid.Empty.ToString())
        {
            category = await _categoryManager.GetById(new Guid(categoryId), updateContext.CancellationToken);
            if (category is null)
            {
                _sessionStateManager.Continue(updateContext.Session, previousState);
                return;
            }
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Category = category;

        _sessionStateManager.Continue(updateContext.Session, WorkflowState.SendInputTransactionDate);
    }
}