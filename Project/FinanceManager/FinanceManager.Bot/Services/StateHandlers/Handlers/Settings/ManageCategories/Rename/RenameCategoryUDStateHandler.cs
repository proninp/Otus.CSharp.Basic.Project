using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Rename;
public sealed class RenameCategoryUDStateHandler : UnderDevelopmentStateHandler
{
    private readonly ISessionStateManager _sessionStateManager;

    public RenameCategoryUDStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
        : base(messageManager)
    {
        _sessionStateManager = sessionStateManager;
    }

    public override string MessageText => $"Rename category feature is under development {Emoji.Rocket.GetSymbol()}";

    public override async Task<bool> Navigate(BotUpdateContext context) =>
        await _sessionStateManager.ToManageCategoriesMenu(context.Session);
}