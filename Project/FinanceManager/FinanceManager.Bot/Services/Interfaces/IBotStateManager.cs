using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IBotStateManager
{
    void HandleMessage(Message message);
}
