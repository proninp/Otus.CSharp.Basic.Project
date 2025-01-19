using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public class HistoryInlineKeyBoardProvider : IHistoryInlineKeyBoardProvider
{
    private readonly IMessageManager _messageManager;

    public HistoryInlineKeyBoardProvider(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public InlineKeyboardMarkup GetKeyboard(BotUpdateContext updateContext)
    {
        var newer = HistoryCommand.Newer;
        var older = HistoryCommand.Older;
        var menu = HistoryCommand.Memu;

        var context = updateContext.Session.GetHistoryContext();

        var pagesCount = (int)Math.Ceiling((double)context.TransactionsCount / context.PageSize) - 1;
        var isNewerAvailable = context.PageIndex > 0;
        var isOlderAvailable = context.PageIndex < pagesCount;

        var callbackData = new CallbackData(updateContext, menu.ToString());
        var menuButton = _messageManager.CreateInlineButton(callbackData, $"{menu.GetSymbol()} {menu.GetDescription()}");

        if (!(isNewerAvailable || isOlderAvailable))
            return new InlineKeyboardMarkup(menuButton);

        var controlButtons = new List<InlineKeyboardButton>();

        if (isNewerAvailable)
        {
            callbackData.Data = newer.ToString();
            controlButtons.Add(
                _messageManager.CreateInlineButton(callbackData, $"{newer.GetSymbol()} {newer.GetDescription()}"));
        }

        if (isOlderAvailable)
        {
            callbackData.Data = older.ToString();
            controlButtons.Add(
                _messageManager.CreateInlineButton(callbackData, $"{older.GetSymbol()} {older.GetDescription()}"));
        }

        var buttons = new InlineKeyboardButton[][]
        {
            controlButtons.ToArray(),
            [menuButton]
        };

        return new InlineKeyboardMarkup(buttons);
    }
}
