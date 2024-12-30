using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IBotStateManager
{
    Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken);
}
