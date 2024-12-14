using FinanceManager.Bot.Models;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Interfaces;
public interface ICommandStateHandler
{
    Task HandleStateAsync(UserSession userSession, Message message);
}
