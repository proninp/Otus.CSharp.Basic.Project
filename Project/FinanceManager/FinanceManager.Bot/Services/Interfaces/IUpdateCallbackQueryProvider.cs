using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUpdateCallbackQueryProvider
{
    Task<CallbackQuery> GetCallbackQuery(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}