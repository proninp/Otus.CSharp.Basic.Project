using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Delete;
public sealed class DeleteCategoryComplitionStateHandler : CompleteStateHandler
{
    private readonly ICategoryManager _categoryManager;

    public DeleteCategoryComplitionStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ICategoryManager categoryManager)
        : base(messageManager, sessionStateManager)
    {
        _categoryManager = categoryManager;
    }

    private protected override async Task HandleCompleteAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetDeleteCategoryContext();
        if (!context.IsConfirm)
        {
            await _messageManager.SendErrorMessage(updateContext, "The action has been canceled.");
            return;
        }

        await _categoryManager.Delete(context.Category!.Id, updateContext.CancellationToken);
        await _messageManager.SendApproveMessage(updateContext, $"Category {context.Category.Title} has been deleted");
    }
}
