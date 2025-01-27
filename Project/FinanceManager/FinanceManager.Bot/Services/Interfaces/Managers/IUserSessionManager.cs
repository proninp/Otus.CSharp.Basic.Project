using FinanceManager.Bot.Models;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.Managers;
public interface IUserSessionManager
{
    Task<UserSession> InstantiateSession(User from, CancellationToken cancellationToken);

    Task<int> CleanupExpiredSessions(CancellationToken cancellationToken);
}
