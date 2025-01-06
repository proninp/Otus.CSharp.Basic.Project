using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public class UserSessionProvider : IUserSessionProvider
{
    private readonly IUserSessionRegistry _userSessionRegistry;
    private readonly IUserSessionManager _userSessionManager;

    public UserSessionProvider(IUserSessionRegistry userSessionRegistry, IUserSessionManager userSessionManager)
    {
        _userSessionRegistry = userSessionRegistry;
        _userSessionManager = userSessionManager;
    }

    public async Task<UserSession> GetUserSession(User? from, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(from);

        if (!_userSessionRegistry.Sessions.TryGetValue(from.Id, out var userSession))
        {
            userSession = await _userSessionManager.InstantiateSession(from, cancellationToken);
            _userSessionRegistry.Sessions.TryAdd(from.Id, userSession);
        }
        return userSession;
    }
}