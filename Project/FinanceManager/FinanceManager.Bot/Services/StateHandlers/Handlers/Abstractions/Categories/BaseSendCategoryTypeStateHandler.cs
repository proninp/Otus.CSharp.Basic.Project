using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions.Categories;

public abstract class BaseSendCategoryTypeStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;
    private readonly ISessionStateManager _sessionStateManager;

    protected BaseSendCategoryTypeStateHandler(
        IMessageManager messageManager,
        IMenuCallbackHandler menuCallbackProvider,
        ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _menuCallbackProvider = menuCallbackProvider;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var keyboard = CreateInlineKeyboard(updateContext);
        var message = GetMessageText(updateContext.Session);
        if (!await _messageManager.EditLastMessage(updateContext, message, keyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, message, keyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    protected abstract string GetMessageText(UserSession session);

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var keyboardButtons = new List<InlineKeyboardButton[]>
        {
            new InlineKeyboardButton[]
            {
                _messageManager.CreateInlineButton(context, TransactionType.Expense.GetDescription(),
                    $"{Emoji.Expense.GetSymbol()} {TransactionType.Expense.GetDescription()}"),

                _messageManager.CreateInlineButton(context, TransactionType.Income.GetDescription(),
                    $"{Emoji.Income.GetSymbol()} {TransactionType.Income.GetDescription()}")
            }
        };

        var menuButton = _menuCallbackProvider.GetMenuButton(context);
        keyboardButtons.Add([menuButton]);

        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
