using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.UserServices;
public class UserSessionStateManager : IUserSessionStateManager
{
    public void Continue(UserSession session, WorkflowState state, bool isClearContext = false)
    {
        if (isClearContext)
            session.WorkflowContext = null;
        session.PreviousState = session.State;
        session.State = state;
        session.WaitForUserInput = false;
    }

    public bool IsContinue(UserSession session)
    {
        var wait = session.WaitForUserInput;
        session.WaitForUserInput = false;
        return !wait;
    }

    public void ResetSession(UserSession session)
    {
        session.PreviousState = WorkflowState.Default;
        session.State = WorkflowState.Default;
        session.WaitForUserInput = false;
        session.WorkflowContext = null;
    }

    public void Wait(UserSession session)
    {
        session.WaitForUserInput = true;
    }

    public void Wait(UserSession session, WorkflowState state)
    {
        session.PreviousState = session.State;
        session.State = state;
        session.WaitForUserInput = true;
    }
}