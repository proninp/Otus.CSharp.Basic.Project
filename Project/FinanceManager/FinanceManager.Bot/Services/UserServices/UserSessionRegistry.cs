using System.Collections.Concurrent;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices;
public class UserSessionRegistry : IUserSessionRegistry
{
    private readonly ConcurrentDictionary<long, UserSession> _userSessions;

    public ConcurrentDictionary<long, UserSession> Sessions => _userSessions;

    public UserSessionRegistry()
    {
        _userSessions = new();
    }
}
