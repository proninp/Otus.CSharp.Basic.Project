using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
public sealed class SelectSettingsMenuStateHandler : BaseSelectMenuStateHandler
{
    public SelectSettingsMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager,
        IMessageManager messageManager)
        : base(callbackDataProvider, sessionStateManager, messageManager)
    {
    }

    public override Dictionary<string, WorkflowState> MenuStateMapping => new Dictionary<string, WorkflowState>
    {
        { SettingsMenu.ManageCategories.GetKey(), WorkflowState.ManageCategories },
        { SettingsMenu.ManageTransactios.GetKey(), WorkflowState.ManageTransactions },
        { SettingsMenu.ManageAccounts.GetKey(), WorkflowState.ManageAccounts },
    };
}