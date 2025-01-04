using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.MenuHandler;
public class CreateMenuSubStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;

    public CreateMenuSubStateHandler(IChatProvider chatProvider)
    {
        _chatProvider = chatProvider;
    }

    public async Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);

        var inlineKeyboard = CreateInlineKeyboard();

        await botClient.SendMessage(
               chat, "Choose action:",
           parseMode: ParseMode.Html, replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
        return UserSubState.SelectMenu;
    }

    private InlineKeyboardMarkup CreateInlineKeyboard()
    {
        var keyboardButtons = new InlineKeyboardButton[][]
        {
            [
                InlineKeyboardButton.WithCallbackData($"{Enums.Emoji.Remove} Expense", $"{nameof(RegisterExpenseStateHandler)}"),
                InlineKeyboardButton.WithCallbackData($"{Enums.Emoji.Add} Income", $"{nameof(RegisterIncomeStateHandler)}"),
            ],
            [
                InlineKeyboardButton.WithCallbackData($"{Enums.Emoji.History} History", $"{nameof(HistoryStateHandler)}"),
                InlineKeyboardButton.WithCallbackData($"{Enums.Emoji.Settings} Settings", $"{nameof(SettingsStateHandler)}"),
            ]
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
