using System.Collections.Concurrent;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public partial class SessionStateRegistry : ISessionStateRegistry
{
    private readonly ConcurrentDictionary<WorkflowState, WorkflowState> _previousStatesMap;

    private readonly ConcurrentDictionary<WorkflowState, (WorkflowState next, bool isContinue)> _nextStatesMap;

    public SessionStateRegistry()
    {
        _previousStatesMap = new();
        _previousStatesMap.TryAdd(WorkflowState.SelectMenu, WorkflowState.CreateMenu);
        _previousStatesMap.TryAdd(WorkflowState.SelectSettingsMenu, WorkflowState.CreateSettingsMenu);
        _previousStatesMap.TryAdd(WorkflowState.SelectManageCategoriesMenu, WorkflowState.CreateManageCategoriesMenu);

        _nextStatesMap = new();
        _nextStatesMap.TryAdd(WorkflowState.CreateMenu, (WorkflowState.SelectMenu, false));
        _nextStatesMap.TryAdd(WorkflowState.CreateSettingsMenu, (WorkflowState.SelectSettingsMenu, false));
        _nextStatesMap.TryAdd(WorkflowState.CreateManageCategoriesMenu, (WorkflowState.SelectManageCategoriesMenu, false));

        AddCreateCategoryWorkflowPrevious();
        AddCreateCategoryWorkflowNext();
        AddDeleteCategoryWorkflowPrevious();
        AddDeleteCategoryWorkflowNext();
        AddRenameCategoryWorkflowPrevious();
        AddRenameCategoryWorkflowNext();

        AddRegisterTransactionWorkflowPrevious();
        AddRegisterTransactionWorkflowNext();

        AddCreateAccountWorkflowPrevious();
        AddCreateAccountWorkflowNext();
    }

    public ConcurrentDictionary<WorkflowState, WorkflowState> PreviousStatesMap => _previousStatesMap;

    public ConcurrentDictionary<WorkflowState, (WorkflowState next, bool isContinue)> NextStatesMap => _nextStatesMap;
}
