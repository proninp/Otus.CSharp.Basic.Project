using System.Collections.Concurrent;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public class UserSessionProvider : IUserSessionProvider
{
    private readonly ConcurrentDictionary<long, UserSession> _userSessions;
    private readonly IUserSessionManager _userSessionManager;

    public UserSessionProvider(IUserSessionManager userSessionManager)
    {
        _userSessions = new();
        _userSessionManager = userSessionManager;
    }

    public async Task<UserSession> GetUserSession(User? from, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(from);

        if (!_userSessions.TryGetValue(from.Id, out var userSession))
        {
            userSession = await _userSessionManager.InstantiateSession(from, cancellationToken);
            _userSessions.TryAdd(from.Id, userSession);
        }
        return userSession;
    }
}
