using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;
public sealed class DeleteCategoryStartStateHandler : UnderDevelopmentStateHandler
{
    private readonly ISessionStateManager _sessionStateManager;

    public DeleteCategoryStartStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
        : base(messageManager)
    {
        _sessionStateManager = sessionStateManager;
    }

    public override string MessageText => $"Ddelete category feature is under development {Emoji.Rocket.GetSymbol()}";

    public override async Task<bool> Navigate(BotUpdateContext updateContext) =>
        await _sessionStateManager.ToManageCategoriesMenu(updateContext.Session);
}
