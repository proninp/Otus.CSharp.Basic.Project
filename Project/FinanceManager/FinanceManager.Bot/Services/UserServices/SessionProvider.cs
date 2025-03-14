using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Core.Options;
using FinanceManager.Redis.Services.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public sealed class SessionProvider : ISessionProvider
{
    private readonly ISessionRegistry _userSessionRegistry;
    private readonly ISessionManager _userSessionManager;
    private readonly IRedisCacheService _redisCacheService;
    private readonly AppSettings _options;


    public SessionProvider(
        ISessionRegistry userSessionRegistry,
        ISessionManager userSessionManager,
        IRedisCacheService redisCacheService,
        IOptionsSnapshot<AppSettings> options)
    {
        _userSessionRegistry = userSessionRegistry;
        _userSessionManager = userSessionManager;
        _redisCacheService = redisCacheService;
        _options = options.Value;
    }

    public async Task<UserSession> GetUserSessionAsync(User? from, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(from);

        if (!_userSessionRegistry.Sessions.TryGetValue(from.Id, out var userSession))
        {
            userSession = await _redisCacheService.GetDataAsync<UserSession>(from.Id.ToString());

            if (userSession is null)
            {
                userSession = await _userSessionManager.InstantiateSession(from, cancellationToken);
                await _redisCacheService.SaveDataAsync(
                    from.Id.ToString(), userSession, TimeSpan.FromMinutes(_options.RedisUserSessionExpirationMinutes));
            }

            _userSessionRegistry.Sessions.TryAdd(from.Id, userSession);
        }

        userSession.LastActivity = DateTime.UtcNow;
        return userSession;
    }
}