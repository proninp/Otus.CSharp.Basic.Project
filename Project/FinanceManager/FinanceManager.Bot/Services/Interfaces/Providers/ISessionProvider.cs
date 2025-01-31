using FinanceManager.Bot.Models;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface ISessionProvider
{
    Task<UserSession> GetUserSession(User? from, CancellationToken cancellationToken);
}
