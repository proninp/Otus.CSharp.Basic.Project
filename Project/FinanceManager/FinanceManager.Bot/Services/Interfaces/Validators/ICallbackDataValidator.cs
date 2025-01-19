using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.Validators;
public interface ICallbackDataValidator
{
    Task<bool> Validate(
        BotUpdateContext updateContext, CallbackData callbackData, bool isSendInformationMessage = false);
}