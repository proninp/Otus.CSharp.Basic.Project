using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.Providers;
public interface IChatProvider
{
    Chat GetChat(Update update);
}
