using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IStateHandler
{
    Task HandleAsync(BotUpdateContext updateContext);
}