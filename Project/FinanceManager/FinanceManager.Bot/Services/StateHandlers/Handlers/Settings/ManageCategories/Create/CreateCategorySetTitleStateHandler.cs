using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategorySetTitleStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly ICategoryManager _categoryManager;

    public CreateCategorySetTitleStateHandler(
        IUpdateMessageProvider messageProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ICategoryManager categoryManager)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _categoryManager = categoryManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
            return false;

        var categoryTitle = message.Text;

        await _messageManager.DeleteLastMessage(updateContext);

        if (string.IsNullOrEmpty(categoryTitle))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "You must specify the title of the new category. Please try again.");
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        if (!categoryTitle.Any(c => char.IsLetterOrDigit(c)))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The category title must contain at least one letter or digit.");
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        if (await _categoryManager.ExistsByTittle(updateContext.Session.Id, categoryTitle, updateContext.CancellationToken))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "A category with that title already exists.");
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        var context = updateContext.Session.GetCreateCategoryContext();
        context.CategoryTitle = categoryTitle;
        return await _sessionStateManager.Next(updateContext.Session);
    }
}