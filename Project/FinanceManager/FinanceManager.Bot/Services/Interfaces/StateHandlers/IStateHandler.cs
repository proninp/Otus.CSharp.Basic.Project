using FinanceManager.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces.StateHandlers;
public interface IStateHandler
{
    Task HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken);
}