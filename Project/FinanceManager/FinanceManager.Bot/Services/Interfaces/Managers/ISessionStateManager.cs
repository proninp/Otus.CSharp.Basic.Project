using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface ISessionStateManager
{
    Task<bool> Reset(UserSession session);

    Task<bool> Next(UserSession session);

    Task<bool> Previous(UserSession session);

    Task<bool> InitAccount(UserSession session);

    Task<bool> ToMenu(UserSession session);

    Task<bool> FromMenu(UserSession session, WorkflowState toState);
}
