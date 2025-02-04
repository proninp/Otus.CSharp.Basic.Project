using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
public sealed class SettingsStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public SettingsStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.DeleteLastMessage(updateContext);
        await _messageManager.SendMessage(updateContext,
            $"The settings feature is under development {Emoji.Rocket.GetSymbol()}");
        return await _sessionStateManager.ToMenu(updateContext.Session);
    }
}
