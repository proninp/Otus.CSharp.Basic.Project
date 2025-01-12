using FinanceManager.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IStateHandler
{
    Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}