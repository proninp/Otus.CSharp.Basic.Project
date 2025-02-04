using System.Runtime.InteropServices;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public class HistoryInlineKeyBoardProvider : IHistoryInlineKeyBoardProvider
{
    private readonly IMessageManager _messageManager;
    private readonly IMenuCallbackHandler _menuCallbackHandler;

    public HistoryInlineKeyBoardProvider(IMessageManager messageManager, IMenuCallbackHandler menuCallbackHandler)
    {
        _messageManager = messageManager;
        _menuCallbackHandler = menuCallbackHandler;
    }

    public InlineKeyboardMarkup GetKeyboard(BotUpdateContext updateContext)
    {
        var newer = NavigationCommand.Newer;
        var older = NavigationCommand.Older;

        var context = updateContext.Session.GetHistoryContext();

        var pagesCount = (int)Math.Ceiling((double)context.TransactionsCount / context.PageSize) - 1;
        var isNewerAvailable = context.PageIndex > 0;
        var isOlderAvailable = context.PageIndex < pagesCount;

        var menuButton = _menuCallbackHandler.GetMenuButton(updateContext);

        if (!(isNewerAvailable || isOlderAvailable))
            return new InlineKeyboardMarkup(menuButton);

        var controlButtons = new List<InlineKeyboardButton>();

        if (isNewerAvailable)
        {
            var callbackData = new CallbackData(updateContext, newer.GetCallbackData());
            controlButtons.Add(
                _messageManager.CreateInlineButton(callbackData, $"{newer.GetSymbol()} {newer.GetDescription()}"));
        }

        if (isOlderAvailable)
        {
            var callbackData = new CallbackData(updateContext, older.GetCallbackData());
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
