using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
public class SelectSettingsMenuStateHandler : IStateHandler
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMessageManager _messageManager;

    public SelectSettingsMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager,
        IMessageManager messageManager)
    {
        _callbackDataProvider = callbackDataProvider;
        _sessionStateManager = sessionStateManager;
        _messageManager = messageManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackData(updateContext, true);
        if (callbackData is null)
            return await _sessionStateManager.Previous(updateContext.Session);

        var stateMapping = new Dictionary<string, WorkflowState>
        {
            { SettingsMenu.ManageCategories.GetKey(), WorkflowState.ManageCategories },
            { SettingsMenu.ManageTransactios.GetKey(), WorkflowState.ManageTransactions },
            { SettingsMenu.ManageAccounts.GetKey(), WorkflowState.ManageAccounts },
        };

        if (!stateMapping.TryGetValue(callbackData.Data, out var newState))
        {
            if (callbackData.MessageId is not null)
                await _messageManager.DeleteMessage(updateContext, callbackData.MessageId.Value);
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        return await _sessionStateManager.FromMenu(updateContext.Session, newState);
    }
}
