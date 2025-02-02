using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Redis.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public sealed class SessionProvider : ISessionProvider
{
    private readonly ISessionRegistry _userSessionRegistry;
    private readonly ISessionManager _userSessionManager;
    private readonly IRedisCacheService _redisCacheService;

    public SessionProvider(
        ISessionRegistry userSessionRegistry,
        ISessionManager userSessionManager,
        IRedisCacheService redisCacheService)
    {
        _userSessionRegistry = userSessionRegistry;
        _userSessionManager = userSessionManager;
        _redisCacheService = redisCacheService;
    }

    public async Task<UserSession> GetUserSession(User? from, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(from);

        if (!_userSessionRegistry.Sessions.TryGetValue(from.Id, out var userSession))
        {
            userSession = await _redisCacheService.GetDataAsync<UserSession>(from.Id.ToString());

            if (userSession is null)
            {
                userSession = await _userSessionManager.InstantiateSession(from, cancellationToken);
                await _redisCacheService.SaveDataAsync(from.Id.ToString(), userSession);
            }

            _userSessionRegistry.Sessions.TryAdd(from.Id, userSession);
        }

        userSession.LastActivity = DateTime.UtcNow;
        return userSession;
    }
}