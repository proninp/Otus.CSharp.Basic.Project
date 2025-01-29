using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Core.Options;
using FinanceManager.Redis.Services.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public class UserSessionProvider : IUserSessionProvider
{
    private readonly IUserSessionRegistry _userSessionRegistry;
    private readonly IUserSessionManager _userSessionManager;
    private readonly IRedisCacheService _redisCacheService;
    private readonly AppSettings _options;
    private readonly ILogger _logger;

    public UserSessionProvider(
        IUserSessionRegistry userSessionRegistry,
        IUserSessionManager userSessionManager,
        IRedisCacheService redisCacheService,
        IOptionsSnapshot<AppSettings> options,
        ILogger logger)
    {
        _userSessionRegistry = userSessionRegistry;
        _userSessionManager = userSessionManager;
        _redisCacheService = redisCacheService;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<UserSession> GetUserSession(User? from, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(from);

        if (!_userSessionRegistry.Sessions.TryGetValue(from.Id, out var userSession))
        {
            userSession = await _redisCacheService.GetData<UserSession>(from.Id.ToString());

            if (userSession is null)
            {
                userSession = await _userSessionManager.InstantiateSession(from, cancellationToken);
                _logger.Information("New session created for user {UserId}", from.Id);
                await _redisCacheService.SaveData(from.Id.ToString(), userSession);
            }

            _userSessionRegistry.Sessions.TryAdd(from.Id, userSession);
        }

        userSession.Expiration = TimeSpan.FromMinutes(_options.InMemoryUserSessionExpirationMinutes);

        return userSession;
    }
}