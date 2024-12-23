using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IBotStateManager
{
    Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
}
