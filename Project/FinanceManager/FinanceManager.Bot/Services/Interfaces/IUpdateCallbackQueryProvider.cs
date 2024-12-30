using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUpdateCallbackQueryProvider
{
    bool GetCallbackQuery(Update update, out CallbackQuery? callbackQuery);
}