using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class BaseChooseMenuStateHandler : IStateHandler
{
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMessageManager _messageManager;

    protected BaseChooseMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager,
        IMessageManager messageManager)
    {
        _callbackDataProvider = callbackDataProvider;
        _sessionStateManager = sessionStateManager;
        _messageManager = messageManager;
    }

    public abstract Dictionary<string, WorkflowState> MenuStateMapping { get; }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackData = await _callbackDataProvider.GetCallbackDataAsync(updateContext, true);
        if (callbackData is null)
            return await _sessionStateManager.Previous(updateContext.Session);

        if (!MenuStateMapping.TryGetValue(callbackData.Data, out var newState))
        {
            if (callbackData.MessageId is not null)
                await _messageManager.DeleteMessageAsync(updateContext, callbackData.MessageId.Value);
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        return await _sessionStateManager.FromMenu(updateContext.Session, newState);
    }
}
