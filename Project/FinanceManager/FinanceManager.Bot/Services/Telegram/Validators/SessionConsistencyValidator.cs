using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.Telegram.Validators;

public sealed class SessionConsistencyValidator : ISessionConsistencyValidator
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public SessionConsistencyValidator(
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _callbackDataProvider = callbackDataProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> ValidateCallbackConsistency(BotUpdateContext updateContext)
    {
        if (updateContext.Update.CallbackQuery is null)
            return true;

        var callbackData = await _callbackDataProvider.GetCallbackDataAsync(updateContext, false);

        var isValidCallback = false;

        if (callbackData != null)
            isValidCallback = callbackData.CallbackSessionId == updateContext.Session.CallbackSessionId;

        if (isValidCallback)
            return true;

        var message = callbackData is null ?
            "The data for processing the selected command could not be received." :
            "The session associated with this action has ended. Please try again.";

        if (updateContext.Update.CallbackQuery.Message?.Id != null)
            await _messageManager.DeleteMessageAsync(updateContext, updateContext.Update.CallbackQuery.Message.Id);

        if (updateContext.Session.LastMessage != null)
            if (updateContext.Session.LastMessage.IsContainsInlineKeyboard)
                await _messageManager.DeleteLastMessageAsync(updateContext);

        await _messageManager.SendErrorMessageAsync(updateContext, message);

        await _sessionStateManager.Previous(updateContext.Session);
        return false;
    }
}
