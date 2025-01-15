using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class CreateMenuStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;

    public CreateMenuStateHandler(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var inlineKeyboard = CreateInlineKeyboard();

        await _messageManager.SendInlineKeyboardMessage(updateContext, "Choose an action:", inlineKeyboard);

        updateContext.Session.Wait(WorkflowState.SelectMenu);
    }

    private InlineKeyboardMarkup CreateInlineKeyboard()
    {
        var keyboardButtons = new InlineKeyboardButton[][]
        {
            [
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.Expense.GetSymbol()} {Enums.Menu.Expense.GetDescription()}", $"{Enums.Menu.Expense.GetKey()}"),
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.Income.GetSymbol()} {Enums.Menu.Income.GetDescription()}", $"{Enums.Menu.Income.GetKey()}"),
            ],
            [
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.History.GetSymbol()} {Enums.Menu.History.GetDescription()}", $"{Enums.Menu.History.GetKey()}"),
                InlineKeyboardButton.WithCallbackData(
                    $"{Emoji.Settings.GetSymbol()} {Enums.Menu.Settings.GetDescription()}", $"{Enums.Menu.Settings.GetKey()}"),
            ]
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
