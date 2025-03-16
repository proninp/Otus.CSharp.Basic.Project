using FinanceManager.Bot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IMenuCallbackHandler
{
    InlineKeyboardButton GetMenuButton(BotUpdateContext context);

    Task<bool> HandleMenuCallbackAsync(BotUpdateContext context);
}