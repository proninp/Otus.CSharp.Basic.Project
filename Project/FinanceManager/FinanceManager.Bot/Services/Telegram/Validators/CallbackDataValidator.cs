using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.Telegram.Validators;
public class CallbackDataValidator : ICallbackDataValidator
{
    private readonly IMessageManager _messageManager;

    public CallbackDataValidator(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public async Task<bool> Validate(
        BotUpdateContext updateContext, CallbackData callbackData, bool isSendInformationMessage = false)
    {
        if (updateContext.Session.CallbackSessionId != callbackData.UserSessionId)
        {
            if (isSendInformationMessage)
            {
                await _messageManager.SendErrorMessage(
                    updateContext, "The session associated with this action has ended. Please try again.");
            }
            return false;
        }

        return true;
    }
}
