using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateManager : ISessionStateManager
{
    private void AddCreateAccountWorkflowPrevious(Dictionary<WorkflowState, WorkflowState> workflowMap)
    {
        workflowMap.Add(WorkflowState.ChooseAccountName, WorkflowState.CreateAccountStart);
        workflowMap.Add(WorkflowState.ChooseCurrency, WorkflowState.SendCurrencies);
        workflowMap.Add(WorkflowState.SetAccountInitialBalance, WorkflowState.SendInputAccountInitialBalance);
    }

    private void AddCreateAccountWorkflowNext(Dictionary<WorkflowState, (WorkflowState next, bool isContinue)> workflowMap)
    {
        workflowMap.Add(WorkflowState.CreateAccountStart, (WorkflowState.ChooseAccountName, false));
        workflowMap.Add(WorkflowState.ChooseAccountName, (WorkflowState.SendCurrencies, true));
        workflowMap.Add(WorkflowState.SendCurrencies, (WorkflowState.ChooseCurrency, false));
        workflowMap.Add(WorkflowState.ChooseCurrency, (WorkflowState.SendInputAccountInitialBalance, true));
        workflowMap.Add(WorkflowState.SendInputAccountInitialBalance, (WorkflowState.SetAccountInitialBalance, false));
        workflowMap.Add(WorkflowState.SetAccountInitialBalance, (WorkflowState.CreateAccountEnd, true));
        workflowMap.Add(WorkflowState.CreateAccountEnd, (WorkflowState.CreateMenu, true));
    }
}