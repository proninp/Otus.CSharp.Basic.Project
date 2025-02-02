using System.Collections.Concurrent;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices;
public sealed class SessionRegistry : ISessionRegistry
{
    private readonly ConcurrentDictionary<long, UserSession> _userSessions;

    public ConcurrentDictionary<long, UserSession> Sessions => _userSessions;

    public IEnumerable<UserSession> ExpiredSessions =>
        _userSessions.Values.Where(s => s.LastActivity + s.Expiration <= DateTime.UtcNow);

    public SessionRegistry()
    {
        _userSessions = new();
    }
}