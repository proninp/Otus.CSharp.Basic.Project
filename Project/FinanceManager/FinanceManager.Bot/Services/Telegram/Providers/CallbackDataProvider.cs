using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public sealed class CallbackDataProvider : ICallbackDataProvider
{
    private readonly IMessageManager _messageManager;

    public CallbackDataProvider(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
    }

    public async Task<CallbackData?> GetCallbackData(
        BotUpdateContext updateContext, bool isDeleteMessageWhenNull = true)
    {
        CallbackData? callbackData = null;
        var callbackQuery = updateContext.Update.CallbackQuery;

        if (callbackQuery is not null)
        {
            var data = callbackQuery.Data ?? string.Empty;
            callbackData = CallbackData.FromRawText(data);
        }

        if (callbackData is null)
        {
            if (isDeleteMessageWhenNull)
                await _messageManager.DeleteLastMessage(updateContext);
        }
        else
        {
            callbackData.MessageId = callbackQuery?.Message?.Id;
        }

        return callbackData;
    }
}