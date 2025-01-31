using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.Telegram.Providers;
internal class CallbackDataProvider : ICallbackDataProvider
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public CallbackDataProvider(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<CallbackData?> GetCallbackData(
        BotUpdateContext updateContext, bool isDeleteMessageWhenNull = true, WorkflowState? continueWithStateWhenNull = null)
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
            if (continueWithStateWhenNull is not null)
                _sessionStateManager.Continue(updateContext.Session, continueWithStateWhenNull.Value);
        }
        else
        {
            callbackData.MessageId = callbackQuery?.Message?.Id;
        }

        return callbackData;
    }
}