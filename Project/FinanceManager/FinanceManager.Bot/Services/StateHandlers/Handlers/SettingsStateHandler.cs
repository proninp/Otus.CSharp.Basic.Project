using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class SettingsStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;

    public SettingsStateHandler(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.SendMessage(updateContext,
            $"The settings feature is under development {Emoji.Rocket.GetSymbol()}");
        updateContext.Session.Continue(WorkflowState.CreateMenu);
    }
}
