using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class SettingsStateHandler : IStateHandler
{
    private readonly IMessageSenderManager _messageSender;

    public SettingsStateHandler(IMessageSenderManager messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        await _messageSender.SendMessage(updateContext,
            $"The settings feature is under development {Emoji.Rocket.GetSymbol()}");
        updateContext.Session.Continue(WorkflowState.CreateMenu);
    }
}
