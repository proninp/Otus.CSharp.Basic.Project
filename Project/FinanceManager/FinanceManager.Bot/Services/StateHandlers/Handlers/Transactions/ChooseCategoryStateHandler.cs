using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class ChooseCategoryStateHandler : IStateHandler
{
    private readonly ICategoryManager _categoryManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;
    private readonly IMessageManager _messageManager;

    public ChooseCategoryStateHandler(
        ICallbackDataProvider callbackQueryProvider,
        ICategoryManager categoryManager,
        ICallbackDataValidator callbackDataValidator,
        IMessageManager messageManager)
    {
        _callbackDataProvider = callbackQueryProvider;
        _categoryManager = categoryManager;
        _callbackDataValidator = callbackDataValidator;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.SendTransactionCategories;
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true, previousState);
        if (callbackData is null)
            return;

        if (!await _callbackDataValidator.Validate(updateContext, callbackData))
        {
            updateContext.Session.Wait();
            return;
        }

        var categoryId = callbackData.Data;

        if (string.IsNullOrEmpty(categoryId))
        {
            await _messageManager.DeleteLastMessage(updateContext);
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