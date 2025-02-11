using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategoryStartStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IMenuCallbackHandler _menuCallbackProvider;

    public CreateCategoryStartStateHandler(
        IMessageManager messageManager, ISessionStateManager sessionStateManager, IMenuCallbackHandler menuCallbackProvider)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _menuCallbackProvider = menuCallbackProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var keyboard = CreateInlineKeyboard(updateContext);
        var messageText = "Please select a new category type:";
        if (!await _messageManager.EditLastMessage(updateContext, messageText, keyboard))
            await _messageManager.SendInlineKeyboardMessage(updateContext, messageText, keyboard);

        return await _sessionStateManager.Next(updateContext.Session);
    }

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
