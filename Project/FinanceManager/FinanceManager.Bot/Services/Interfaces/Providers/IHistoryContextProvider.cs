using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IHistoryContextProvider
{
    Task<HistoryContext?> GetHistoryContexAsync(BotUpdateContext updateContext);
}