using System.Security.Cryptography;
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
public sealed class SessionManager : ISessionManager
{
    private readonly IUserManager _userManager;
    private readonly ISessionRegistry _userSessionRegistry;
    private readonly IRedisCacheService _redisCacheService;
    private readonly AppSettings _options;

    public SessionManager(
        IUserManager userManager,
        ISessionRegistry userSessionRegistry,
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
        var callbackSessionId = GenerateCallbackSessionId();

        return userDto.ToUserSession(callbackSessionId);
    }

    public async Task<int> CleanupExpiredSessions(CancellationToken cancellationToken)
    {
        var expiredSessions = _userSessionRegistry.ExpiredSessions;
        foreach (var session in expiredSessions)
        {
            var ttl = TimeSpan.FromMinutes(_options.RedisUserSessionExpirationMinutes);

            await _redisCacheService.SaveData(session.TelegramId.ToString(), session, ttl);
            _userSessionRegistry.Sessions.TryRemove(session.TelegramId, out var _);
        }
        return expiredSessions.Count();
    }

    private string GenerateCallbackSessionId()
    {
        int maxLength = 6;
        int byteLength = (int)Math.Ceiling(maxLength * 0.75);

        byte[] randomBytes = RandomNumberGenerator.GetBytes(byteLength);

        string base64Token = Convert.ToBase64String(randomBytes);

        base64Token = base64Token.Replace("=", "").Replace("/", "").Replace("+", "");

        return base64Token.Length > maxLength
            ? base64Token.Substring(0, maxLength)
            : base64Token;
    }
}