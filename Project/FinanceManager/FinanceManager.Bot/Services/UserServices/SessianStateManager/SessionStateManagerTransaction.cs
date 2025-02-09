using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateManager : ISessionStateManager
{
    private void AddRegistrTransactionWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.ChooseTransactionCategory, WorkflowState.SendTransactionCategories);
        workflowMap.Add(WorkflowState.SetTransactionDate, WorkflowState.SendInputTransactionDate);
        workflowMap.Add(WorkflowState.SetTransactionAmount, WorkflowState.SendInputTransactionAmount);
        workflowMap.Add(WorkflowState.History, WorkflowState.CreateMenu);
    }

    private void AddRegisterTransactionWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.AddExpense, (WorkflowState.SendTransactionCategories, true));
        workflowMap.Add(WorkflowState.AddIncome, (WorkflowState.SendTransactionCategories, true));
        workflowMap.Add(WorkflowState.SendTransactionCategories, (WorkflowState.ChooseTransactionCategory, false));
        workflowMap.Add(WorkflowState.ChooseTransactionCategory, (WorkflowState.SendInputTransactionDate, true));
        workflowMap.Add(WorkflowState.SendInputTransactionDate, (WorkflowState.SetTransactionDate, false));
        workflowMap.Add(WorkflowState.SetTransactionDate, (WorkflowState.SendInputTransactionAmount, true));
        workflowMap.Add(WorkflowState.SendInputTransactionAmount, (WorkflowState.SetTransactionAmount, false));
        workflowMap.Add(WorkflowState.SetTransactionAmount, (WorkflowState.RegisterTransaction, true));
        workflowMap.Add(WorkflowState.RegisterTransaction, (WorkflowState.CreateMenu, true));
    }
}