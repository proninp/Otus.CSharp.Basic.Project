using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageAccounts;
public sealed class ManageAccountsStateHandler : UnderDevelopmentStateHandler
{
    private readonly ISessionStateManager _sessionStateManager;

    public ManageAccountsStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
        : base(messageManager)
    {
        _sessionStateManager = sessionStateManager;
    }

    public override string MessageText => $"The Manage accoints feature is under development {Emoji.Rocket.GetSymbol()}";

    public override async Task<bool> Navigate(BotUpdateContext updateContext) =>
        await _sessionStateManager.ToSettingsMenu(updateContext.Session);
}