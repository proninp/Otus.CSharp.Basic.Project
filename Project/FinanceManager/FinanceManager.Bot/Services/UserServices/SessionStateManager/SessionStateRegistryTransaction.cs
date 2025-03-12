using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateRegistry
{
    private void AddRegisterTransactionWorkflowPrevious()
    {
        _previousStatesMap.TryAdd(WorkflowState.ChooseTransactionCategory, WorkflowState.SendTransactionCategories);
        _previousStatesMap.TryAdd(WorkflowState.SetTransactionDate, WorkflowState.SendInputTransactionDate);
        _previousStatesMap.TryAdd(WorkflowState.SetTransactionAmount, WorkflowState.SendInputTransactionAmount);
        _previousStatesMap.TryAdd(WorkflowState.History, WorkflowState.CreateMenu);
    }

    private void AddRegisterTransactionWorkflowNext()
    {
        _nextStatesMap.TryAdd(WorkflowState.AddExpense, (WorkflowState.SendTransactionCategories, true));
        _nextStatesMap.TryAdd(WorkflowState.AddIncome, (WorkflowState.SendTransactionCategories, true));
        _nextStatesMap.TryAdd(WorkflowState.SendTransactionCategories, (WorkflowState.ChooseTransactionCategory, false));
        _nextStatesMap.TryAdd(WorkflowState.ChooseTransactionCategory, (WorkflowState.SendInputTransactionDate, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputTransactionDate, (WorkflowState.SetTransactionDate, false));
        _nextStatesMap.TryAdd(WorkflowState.SetTransactionDate, (WorkflowState.SendInputTransactionAmount, true));
        _nextStatesMap.TryAdd(WorkflowState.SendInputTransactionAmount, (WorkflowState.SetTransactionAmount, false));
        _nextStatesMap.TryAdd(WorkflowState.SetTransactionAmount, (WorkflowState.AddTransactionComplete, true));
        _nextStatesMap.TryAdd(WorkflowState.AddTransactionComplete, (WorkflowState.CreateMenu, true));
    }
}
