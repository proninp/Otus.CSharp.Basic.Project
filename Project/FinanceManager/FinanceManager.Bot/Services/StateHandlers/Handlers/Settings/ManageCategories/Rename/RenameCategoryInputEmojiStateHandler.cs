using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Rename;

public sealed class RenameCategoryInputEmojiStateHandler : BaseCategoryInputEmojiStateHandler
{
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public RenameCategoryInputEmojiStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
        : base(messageManager, sessionStateManager)
    {
        _menuCallbackProvider = menuCallbackProvider;
    }

    private protected override InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var buttons = new List<InlineKeyboardButton>();
        var currentEmoji = context.Session.GetRenameCategoryContext().Emoji;
        if (!string.IsNullOrEmpty(currentEmoji))
        {
            buttons.Add(_messageManager.CreateInlineButton(context, currentEmoji, $"Leave unchanged {currentEmoji}"));
        }
        buttons.Add(_messageManager.CreateInlineButton(context, Guid.Empty.ToString(), $"{Emoji.Skip.GetSymbol()} Skip"));
        buttons.Add(_menuCallbackProvider.GetMenuButton(context));

        return new InlineKeyboardMarkup(buttons);
    }
}