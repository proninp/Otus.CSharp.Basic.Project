using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

public abstract class BaseInputCategoryTitleStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    protected BaseInputCategoryTitleStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _menuCallbackProvider = menuCallbackProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var message = GetMessage();
        var menuButton = _menuCallbackProvider.GetMenuButton(updateContext);
        var inlineKeyboard = new InlineKeyboardMarkup(menuButton);

        if (!await _messageManager.EditLastMessageAsync(updateContext, message, inlineKeyboard))
            await _messageManager.SendInlineKeyboardMessageAsync(updateContext, message, inlineKeyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private protected abstract string GetMessage();
}
