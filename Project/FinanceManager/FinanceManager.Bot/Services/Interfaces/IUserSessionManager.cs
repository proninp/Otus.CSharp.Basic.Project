using FinanceManager.Bot.Models;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services;
public interface IUserSessionManager
{
    Task<UserSession> InstantiateSession(User from);
}
