using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.UserServices;
public class SessionStateManager : ISessionStateManager
{
    public bool Previous(UserSession session)
    {
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
        return true;
    }

    public bool Next(UserSession session)
    {
        var stateTransitions = new Dictionary<WorkflowState, (WorkflowState next, bool isContinue)>
        {
            { WorkflowState.CreateMenu, (WorkflowState.SelectMenu, false) },

            { WorkflowState.CreateAccountStart, (WorkflowState.ChooseAccountName, false) },
            { WorkflowState.ChooseAccountName, (WorkflowState.SendCurrencies, true) },
            { WorkflowState.SendCurrencies, (WorkflowState.ChooseCurrency, false) },
            { WorkflowState.ChooseCurrency, (WorkflowState.SendInputAccountInitialBalance, true) },
            { WorkflowState.SendInputAccountInitialBalance, (WorkflowState.SetAccountInitialBalance, false) },
            { WorkflowState.SetAccountInitialBalance, (WorkflowState.CreateAccountEnd, true) },
            { WorkflowState.CreateAccountEnd, (WorkflowState.CreateMenu, true) },
            
            { WorkflowState.AddExpense, (WorkflowState.SendTransactionCategories, true) },
            { WorkflowState.AddIncome, (WorkflowState.SendTransactionCategories, true) },
            { WorkflowState.SendTransactionCategories, (WorkflowState.ChooseTransactionCategory, false) },
            { WorkflowState.ChooseTransactionCategory, (WorkflowState.SendInputTransactionDate, true) },
            { WorkflowState.SendInputTransactionDate, (WorkflowState.SetTransactionDate, false) },
            { WorkflowState.SetTransactionDate, (WorkflowState.SendInputTransactionAmount, true) },
            { WorkflowState.SendInputTransactionAmount, (WorkflowState.SetTransactionAmount, false) },
            { WorkflowState.SetTransactionAmount, (WorkflowState.RegisterTransaction, true) },
            { WorkflowState.RegisterTransaction, (WorkflowState.CreateMenu, true) },

            { WorkflowState.Settings, (WorkflowState.CreateMenu, true) },
        };

        bool result;

        if (stateTransitions.TryGetValue(session.State, out var sessionBehavior))
        {
            session.State = sessionBehavior.next;
            result = sessionBehavior.isContinue;
        }
        else
        {
            result = Reset(session);
        }
        return result;
    }

    public bool ToMenu(UserSession session) =>
        Continue(session, WorkflowState.CreateMenu);

    public bool InitAccount(UserSession session) =>
        Continue(session, WorkflowState.CreateAccountStart);

    public bool FromMenu(UserSession session, WorkflowState toState) =>
        Continue(session, toState);
    
    public bool Reset(UserSession session) =>
        Continue(session, WorkflowState.Default);

    private bool Continue(UserSession session, WorkflowState withState)
    {
        session.WorkflowContext = null;
        session.State = withState;
        return true;
    }
}