using System.Collections.Concurrent;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces;
public interface ISessionRegistry
{
    public ConcurrentDictionary<long, UserSession> Sessions { get; }

    public IEnumerable<UserSession> ExpiredSessions {  get; }
}
