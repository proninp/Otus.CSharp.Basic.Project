using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class UnderDevelopmentStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;

    protected UnderDevelopmentStateHandler(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!await _messageManager.EditLastMessageAsync(updateContext, MessageText))
        {
            await _messageManager.DeleteLastMessageAsync(updateContext);
            await _messageManager.SendMessageAsync(updateContext, MessageText);
        }
        updateContext.Session.LastMessage = null;
        return await Navigate(updateContext);
    }

    public abstract string MessageText { get; }

    public abstract Task<bool> Navigate(BotUpdateContext updateContext);
}
