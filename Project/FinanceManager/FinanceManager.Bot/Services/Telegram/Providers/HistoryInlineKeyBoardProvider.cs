using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public class HistoryInlineKeyBoardProvider : IHistoryInlineKeyBoardProvider
{
    public InlineKeyboardMarkup GetKeyboard(BotUpdateContext updateContext)
    {
        var next = HistoryCommand.Next;
        var previous = HistoryCommand.Previous;
        var menu = HistoryCommand.Memu;

        var context = updateContext.Session.GetHistoryContext();

        var isNextAvailable = context.PageIndex < context.TransactionsCount / context.PageSize;
        var isPreviousAvailable = context.PageIndex > 0;

        var backButton = InlineKeyboardButton.WithCallbackData(
                    $"{menu.GetSymbol()} {menu.GetDescription()}", menu.ToString());

        if (!(isNextAvailable || isPreviousAvailable))
            return new InlineKeyboardMarkup(backButton);

        var controlButtons = new List<InlineKeyboardButton>();

        if (isNextAvailable)
        {
            controlButtons.Add(
                InlineKeyboardButton.WithCallbackData(
                    $"{next.GetSymbol()} {next.GetDescription()}", next.ToString()));
        }

        if (isPreviousAvailable)
        {
            controlButtons.Add(
                InlineKeyboardButton.WithCallbackData(
                    $"{previous.GetSymbol()} {previous.GetDescription()}", previous.ToString()));
        }

        var buttons = new InlineKeyboardButton[][]
        {
            controlButtons.ToArray(),
            [backButton]
        };

        return new InlineKeyboardMarkup(buttons);
    }
}
