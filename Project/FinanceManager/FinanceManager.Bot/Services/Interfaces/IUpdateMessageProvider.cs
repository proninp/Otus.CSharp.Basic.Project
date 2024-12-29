using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IUpdateMessageProvider
{
    Task<Message> GetMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}