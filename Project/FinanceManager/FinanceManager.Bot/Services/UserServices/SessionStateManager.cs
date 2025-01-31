using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.UserServices;
public class SessionStateManager : ISessionStateManager
{
    public void Previous(UserSession session)
    {
        session.WaitForUserInput = false;

        var stateTransitions = new Dictionary<WorkflowState, WorkflowState>
        {
            { WorkflowState.SelectMenu, WorkflowState.CreateMenu },
            { WorkflowState.ChooseAccountName, WorkflowState.CreateAccountStart },
            { WorkflowState.ChooseCurrency, WorkflowState.SendCurrencies },
            { WorkflowState.SetAccountInitialBalance, WorkflowState.SendInputAccountInitialBalance },
            { WorkflowState.ChooseTransactionCategory, WorkflowState.SendTransactionCategories },
            { WorkflowState.SetTransactionDate, WorkflowState.SendInputTransactionDate },
            { WorkflowState.SetTransactionAmount, WorkflowState.SendInputTransactionAmount },
            { WorkflowState.History, WorkflowState.CreateMenu },
            { WorkflowState.Settings, WorkflowState.CreateMenu }
        };

        if (stateTransitions.TryGetValue(session.State, out var toState))
        {
            session.State = toState;
        }
        else
        {
            Reset(session);
        }
    }

    public void Next(UserSession session)
    {
        var stateTransitions = new Dictionary<WorkflowState, (WorkflowState next, bool wait)>
        {
            { WorkflowState.CreateMenu, (WorkflowState.SelectMenu, true) },

            { WorkflowState.CreateAccountStart, (WorkflowState.ChooseAccountName, true) },
            { WorkflowState.ChooseAccountName, (WorkflowState.SendCurrencies, false) },
            { WorkflowState.SendCurrencies, (WorkflowState.ChooseCurrency, true) },
            { WorkflowState.ChooseCurrency, (WorkflowState.SendInputAccountInitialBalance, false) },
            { WorkflowState.SendInputAccountInitialBalance, (WorkflowState.SetAccountInitialBalance, true) },
            { WorkflowState.SetAccountInitialBalance, (WorkflowState.CreateAccountEnd, false) },
            { WorkflowState.CreateAccountEnd, (WorkflowState.CreateMenu, false) },
            
            { WorkflowState.AddExpense, (WorkflowState.SendTransactionCategories, false) },
            { WorkflowState.AddIncome, (WorkflowState.SendTransactionCategories, false) },
            { WorkflowState.SendTransactionCategories, (WorkflowState.ChooseTransactionCategory, true) },
            { WorkflowState.ChooseTransactionCategory, (WorkflowState.SendInputTransactionDate, false) },
            { WorkflowState.SendInputTransactionDate, (WorkflowState.SetTransactionDate, true) },
            { WorkflowState.SetTransactionDate, (WorkflowState.SendInputTransactionAmount, false) },
            { WorkflowState.SendInputTransactionAmount, (WorkflowState.SetTransactionAmount, true) },
            { WorkflowState.SetTransactionAmount, (WorkflowState.RegisterTransaction, false) },
            { WorkflowState.RegisterTransaction, (WorkflowState.CreateMenu, false) },

            { WorkflowState.Settings, (WorkflowState.CreateMenu, false) },
        };


        if (stateTransitions.TryGetValue(session.State, out var sessionBehavior))
        {
            session.State = sessionBehavior.next;
            session.WaitForUserInput = sessionBehavior.wait;
        }
        else
        {
            Reset(session);
        }
    }

    public void ToMenu(UserSession session)
    {
        session.WorkflowContext = null;
        session.State = WorkflowState.CreateMenu;
        session.WaitForUserInput = false;
    }

    public void InitAccount(UserSession session)
    {
        session.WorkflowContext = null;
        session.State = WorkflowState.CreateAccountStart;
        session.WaitForUserInput = false;
    }

    public void FromMenu(UserSession session, WorkflowState newState) =>
        Continue(session, newState, true);

    public bool IsContinue(UserSession session)
    {
        var wait = session.WaitForUserInput;
        session.WaitForUserInput = false;
        return !wait;
    }

    public void Reset(UserSession session)
    {
        session.State = WorkflowState.Default;
        session.WaitForUserInput = false;
        session.WorkflowContext = null;
    }

    public void Wait(UserSession session)
    {
        session.WaitForUserInput = true;
    }

    private void Continue(UserSession session, WorkflowState state, bool isClearContext = false)
    {
        if (isClearContext)
            session.WorkflowContext = null;
        session.State = state;
        session.WaitForUserInput = false;
    }
}