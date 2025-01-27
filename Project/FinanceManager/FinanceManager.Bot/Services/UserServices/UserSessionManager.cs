using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Core.Options;
using FinanceManager.Redis.Services.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.UserServices;
public sealed class UserSessionManager : IUserSessionManager
{
    private readonly IUserManager _userManager;
    private readonly IUserSessionRegistry _userSessionRegistry;
    private readonly IRedisCacheService _redisCacheService;
    private readonly AppSettings _options;

    public UserSessionManager(
        IUserManager userManager,
        IUserSessionRegistry userSessionRegistry,
        IRedisCacheService redisCacheService,
        IOptionsSnapshot<AppSettings> options)
    {
        _userManager = userManager;
        _userSessionRegistry = userSessionRegistry;
        _redisCacheService = redisCacheService;
        _options = options.Value;
    }

    public async Task<UserSession> InstantiateSession(User from, CancellationToken cancellationToken)
    {
        var userDto = await _userManager.GetByTelegramId(from.Id, cancellationToken);
        if (userDto is null)
        {
            var userCommand = new CreateUserDto
            {
                TelegramId = from.Id,
                Username = from.Username,
                Firstname = from.FirstName,
                Lastname = from.LastName
            };
            userDto = await _userManager.Create(userCommand, cancellationToken);
        }
        return userDto.ToUserSession();
    }

    public async Task<int> CleanupExpiredSessions(CancellationToken cancellationToken)
    {
        var expiredSessions = _userSessionRegistry.Sessions.Where(s => s.Value.CreatedAt + s.Value.Expiration <= DateTime.UtcNow);
        foreach (var kvp in expiredSessions)
        {
            var ttl = TimeSpan.FromMinutes(_options.RedisUserSessionExpirationMinutes);
            await _redisCacheService.SaveData(kvp.Key.ToString(), kvp.Value, ttl);
            _userSessionRegistry.Sessions.TryRemove(kvp.Key, out var session);
        }
        return expiredSessions.Count();
    }
}