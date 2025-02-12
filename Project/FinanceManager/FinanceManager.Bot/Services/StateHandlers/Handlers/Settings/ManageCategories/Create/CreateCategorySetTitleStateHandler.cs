using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategorySetTitleStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly ICategoryTitleValidator _categoryTitleValidator;

    public CreateCategorySetTitleStateHandler(
        IUpdateMessageProvider messageProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ICategoryTitleValidator categoryTitleValidator)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _categoryTitleValidator = categoryTitleValidator;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
            return false;

        var categoryTitle = message.Text;

        await _messageManager.DeleteLastMessage(updateContext);

        if (!await _categoryTitleValidator.Validate(updateContext, categoryTitle))
            return await _sessionStateManager.Previous(updateContext.Session);

        var context = updateContext.Session.GetCreateCategoryContext();
        context.CategoryTitle = categoryTitle;
        return await _sessionStateManager.Next(updateContext.Session);
    }
}