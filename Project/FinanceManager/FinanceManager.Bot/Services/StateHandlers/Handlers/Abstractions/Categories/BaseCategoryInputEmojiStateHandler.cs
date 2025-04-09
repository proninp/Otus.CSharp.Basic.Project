using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

public abstract class BaseCategoryInputEmojiStateHandler : IStateHandler
{
    private protected readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    protected BaseCategoryInputEmojiStateHandler(
        IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.SendInlineKeyboardMessageAsync(
        updateContext,
            $"Please specify the emoji that will be associated with the category:",
            CreateInlineKeyboard(updateContext));

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private protected abstract InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context);
}
