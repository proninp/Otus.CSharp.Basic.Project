using System.Collections.Concurrent;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUserSessionRegistry
{
    public ConcurrentDictionary<long, UserSession> Sessions { get; }
}
