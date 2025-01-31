using System.Collections.Concurrent;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices;
public class SessionRegistry : ISessionRegistry
{
    private readonly ConcurrentDictionary<long, UserSession> _userSessions;

    public ConcurrentDictionary<long, UserSession> Sessions => _userSessions;

    public IEnumerable<UserSession> ExpiredSessions =>
        _userSessions.Values.Where(s => s.CreatedAt + s.Expiration <= DateTime.UtcNow);

    public SessionRegistry()
    {
        _userSessions = new();
    }
}
