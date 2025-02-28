using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories;
public sealed class SelectManageCategoriesMenuStateHandler : BaseChooseMenuStateHandler
{
    public SelectManageCategoriesMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager,
        IMessageManager messageManager)
        : base(callbackDataProvider, sessionStateManager, messageManager)
    {
    }

    public override Dictionary<string, WorkflowState> MenuStateMapping => new Dictionary<string, WorkflowState>
    {
        { ManageCategoriesMenu.Add.GetKey(), WorkflowState.SendNewCategoryType },
        { ManageCategoriesMenu.Delete.GetKey(), WorkflowState.SendChooseDeletingCategoryType },
        { ManageCategoriesMenu.Rename.GetKey(), WorkflowState.SendChooseRenamingCategory },
    };
}