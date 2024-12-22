using FinanceManager.Bot.Models;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUserSessionProvider
{
    Task<UserSession> GetUserSession(User? from, CancellationToken cancellationToken);
}
