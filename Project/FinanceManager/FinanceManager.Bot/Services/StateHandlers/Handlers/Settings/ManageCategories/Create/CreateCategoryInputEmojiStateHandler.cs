﻿using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategoryInputEmojiStateHandler : BaseCategoryInputEmojiStateHandler
{
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public CreateCategoryInputEmojiStateHandler(
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IMenuCallbackHandler menuCallbackProvider)
        : base(messageManager, sessionStateManager)
    {
        _menuCallbackProvider = menuCallbackProvider;
    }

    private protected override InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var buttons = new List<InlineKeyboardButton>()
        {
            _messageManager.CreateInlineButton(context, Guid.Empty.ToString(), $"{Emoji.Skip.GetSymbol()} Skip"),
            _menuCallbackProvider.GetMenuButton(context)
        };
        return new InlineKeyboardMarkup(buttons);
    }
        
}