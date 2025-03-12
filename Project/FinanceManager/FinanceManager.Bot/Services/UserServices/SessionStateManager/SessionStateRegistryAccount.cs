using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateRegistry
{
    private void AddCreateAccountWorkflowPrevious()
    {
        _previousStatesMap.TryAdd(WorkflowState.ChooseAccountName, WorkflowState.CreateAccountStart);
        _previousStatesMap.TryAdd(WorkflowState.ChooseCurrency, WorkflowState.SendCurrencies);
        _previousStatesMap.TryAdd(WorkflowState.SetAccountInitialBalance, WorkflowState.SendInputAccountInitialBalance);
    }

    private void AddCreateAccountWorkflowNext()
    {
        _nextStatesMap.TryAdd(WorkflowState.CreateAccountStart, (WorkflowState.ChooseAccountName, false));
        _nextStatesMap.TryAdd(WorkflowState.ChooseAccountName, (WorkflowState.SendCurrencies, true));
        _nextStatesMap.TryAdd(WorkflowState.SendCurrencies, (WorkflowState.ChooseCurrency, false));
        _nextStatesMap.TryAdd(WorkflowState.ChooseCurrency, (WorkflowState.SendInputAccountInitialBalance, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputAccountInitialBalance, (WorkflowState.SetAccountInitialBalance, false));
        _nextStatesMap.TryAdd(WorkflowState.SetAccountInitialBalance, (WorkflowState.CreateAccountComplete, true));
        _nextStatesMap.TryAdd(WorkflowState.CreateAccountComplete, (WorkflowState.CreateMenu, true));
    }
}
