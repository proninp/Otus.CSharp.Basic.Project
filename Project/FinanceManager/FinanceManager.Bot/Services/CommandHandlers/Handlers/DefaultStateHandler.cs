using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class DefaultStateHandler : IStateHandler
{
    public Task<UserState?> HandleStateAsync(UserSession userSession, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
