using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IBotStateManager
{
    Task HandleMessageAsync(Message message, CancellationToken cancellationToken);
}
