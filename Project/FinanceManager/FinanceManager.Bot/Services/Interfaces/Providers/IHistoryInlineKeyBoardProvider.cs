using FinanceManager.Bot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IHistoryInlineKeyBoardProvider
{
    InlineKeyboardMarkup GetKeyboard(BotUpdateContext updateContext);
}