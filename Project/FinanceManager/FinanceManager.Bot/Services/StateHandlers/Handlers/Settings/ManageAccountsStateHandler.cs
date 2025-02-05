using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
public class ManageAccountsStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public ManageAccountsStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.DeleteLastMessage(updateContext);
        await _messageManager.SendMessage(updateContext,
            $"The Manage accoints feature is under development {Emoji.Rocket.GetSymbol()}");
        updateContext.Session.LastMessage = null;
        return await _sessionStateManager.ToSettingsMenu(updateContext.Session);
    }
}