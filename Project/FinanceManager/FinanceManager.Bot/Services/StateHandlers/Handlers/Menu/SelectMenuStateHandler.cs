using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public sealed class SelectMenuStateHandler : IStateHandler
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ISessionStateManager _sessionStateManager;

    public SelectMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager)
    {
        _callbackDataProvider = callbackDataProvider;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true);
        if (callbackData is null)
            return _sessionStateManager.Previous(updateContext.Session);

        var stateMapping = new Dictionary<string, WorkflowState>
        {
            { MainMenu.Expense.GetKey(), WorkflowState.AddExpense },
            { MainMenu.Income.GetKey(), WorkflowState.AddIncome },
            { MainMenu.History.GetKey(), WorkflowState.History },
            { MainMenu.Settings.GetKey(), WorkflowState.Settings }
        };

        if (!stateMapping.TryGetValue(callbackData.Data, out var newState))
            newState = updateContext.Session.State;

        return _sessionStateManager.FromMenu(updateContext.Session, newState);
    }
}