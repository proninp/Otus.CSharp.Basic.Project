using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface ICallbackDataProvider
{
    Task<CallbackData?> GetCallbackData(
        BotUpdateContext updateContext, bool isDeleteMessageWhenNull = true);
}