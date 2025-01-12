using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class CreateMenuStateHandler : IStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public CreateMenuStateHandler(IChatProvider chatProvider, IMessageSenderManager messageSender)
    {
        _chatProvider = chatProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_chatProvider.GetChat(update, out var chat))
            return;

        var inlineKeyboard = CreateInlineKeyboard();

        await _messageSender.SendInlineKeyboardMessage(
            botClient, chat, "Choose an action:", inlineKeyboard, cancellationToken);

        session.Wait(WorkflowState.SelectMenu);
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
