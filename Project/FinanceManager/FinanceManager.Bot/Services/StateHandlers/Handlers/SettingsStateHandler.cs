using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class SettingsStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly IUserSessionStateManager _sessionStateManager;

    public SettingsStateHandler(IMessageManager messageManager, IUserSessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.DeleteLastMessage(updateContext);
        await _messageManager.SendMessage(updateContext,
            $"The settings feature is under development {Emoji.Rocket.GetSymbol()}");
        _sessionStateManager.Continue(updateContext.Session, WorkflowState.CreateMenu);
    }
}
