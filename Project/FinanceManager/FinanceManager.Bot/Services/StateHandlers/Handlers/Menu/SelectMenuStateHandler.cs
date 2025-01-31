using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class SelectMenuStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;
    private readonly ISessionStateManager _sessionStateManager;

    public SelectMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ICallbackDataValidator callbackDataValidator,
        ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _callbackDataProvider = callbackDataProvider;
        _callbackDataValidator = callbackDataValidator;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.CreateMenu;
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true, previousState);
        if (callbackData is null)
            return;

        var stateMapping = new Dictionary<string, WorkflowState>
        {
            { MainMenu.Expense.GetKey(), WorkflowState.AddExpense },
            { MainMenu.Income.GetKey(), WorkflowState.AddIncome },
            { MainMenu.History.GetKey(), WorkflowState.History },
            { MainMenu.Settings.GetKey(), WorkflowState.Settings }
        };

        if (!stateMapping.TryGetValue(callbackData.Data, out var newState))
            newState = updateContext.Session.State;

        _sessionStateManager.Continue(updateContext.Session, newState, true);
    }
}
