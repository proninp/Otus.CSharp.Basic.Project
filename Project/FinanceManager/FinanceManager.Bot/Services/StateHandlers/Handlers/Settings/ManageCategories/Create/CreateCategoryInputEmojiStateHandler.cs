using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategoryInputEmojiStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public CreateCategoryInputEmojiStateHandler(
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
        await _messageManager.SendInlineKeyboardMessageAsync(
        updateContext,
            $"Please specify the emoji that will be associated with the category:",
            CreateInlineKeyboard(updateContext));

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var menuButton = _menuCallbackProvider.GetMenuButton(context);
        var skip = _messageManager.CreateInlineButton(context, Guid.Empty.ToString(), $"{Emoji.Skip.GetSymbol()} Skip");
        var buttons = new List<InlineKeyboardButton>()
        {
            _messageManager.CreateInlineButton(context, Guid.Empty.ToString(), $"{Emoji.Skip.GetSymbol()} Skip"),
            _menuCallbackProvider.GetMenuButton(context)
        };
        return new InlineKeyboardMarkup(buttons);
    }
        
}