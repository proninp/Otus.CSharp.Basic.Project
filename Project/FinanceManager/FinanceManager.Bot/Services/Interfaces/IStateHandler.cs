using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface IStateHandler
{
    Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken);
}
