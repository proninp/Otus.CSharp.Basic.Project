using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IChatProvider
{
    Chat GetChat(Update update);
}
