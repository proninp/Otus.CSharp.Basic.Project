using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

public abstract class BaseSetCategoryTitleStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly ICategoryTitleValidator _categoryTitleValidator;

    protected BaseSetCategoryTitleStateHandler(
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

        await _messageManager.DeleteLastMessageAsync(updateContext);

        var validationResult = await _categoryTitleValidator.ValidateAsync(updateContext, message.Text);

        if (!validationResult.isValid)
            return await _sessionStateManager.Previous(updateContext.Session);

        SetNewTitleToContext(updateContext.Session, validationResult.newTitle);
        return await _sessionStateManager.Next(updateContext.Session);
    }

    private protected abstract void SetNewTitleToContext(UserSession session, string newTitle);

}
