using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface ISessionStateManager
{
    void Reset(UserSession session);

    bool IsContinue(UserSession session);

    void Wait(UserSession session);

    void Next(UserSession session);

    void Previous(UserSession session);

    void InitAccount(UserSession session);

    void ToMenu(UserSession session);

    void FromMenu(UserSession session, WorkflowState newState);
}
