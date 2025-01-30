using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IUserSessionStateManager
{
    void ResetSession(UserSession session);

    bool IsContinue(UserSession session);

    void Wait(UserSession session);

    void Wait(UserSession session, WorkflowState state);

    void Continue(UserSession session, WorkflowState state, bool isClearContext = false);
}
