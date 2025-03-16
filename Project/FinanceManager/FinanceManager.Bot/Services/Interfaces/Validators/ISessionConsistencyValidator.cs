using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.Validators;
public interface ISessionConsistencyValidator
{
    Task<bool> ValidateCallbackConsistencyAsync(BotUpdateContext updateContext);
}
