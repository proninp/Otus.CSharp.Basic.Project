using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISessionStateManager
{
    bool Reset(UserSession session);

    bool Next(UserSession session);

    bool Previous(UserSession session);

    bool InitAccount(UserSession session);

    bool ToMenu(UserSession session);

    bool FromMenu(UserSession session, WorkflowState toState);
}
