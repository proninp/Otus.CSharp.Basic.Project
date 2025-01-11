using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers;

public class StateHandler : IStateHandler
{
    private readonly ISubStateFactoryProvider _subStateFactoryProvider;

    protected StateHandler(ISubStateFactoryProvider subStateFactoryProvider)
    {
        _subStateFactoryProvider = subStateFactoryProvider;
    }

    public async Task HandleStateAsync(UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var subStateHandlerFactory = _subStateFactoryProvider.GetSubStateFactory(userSession.UserState);
        var subStateHandler = subStateHandlerFactory.GetSubStateHandler(userSession.UserState);
        await subStateHandler.HandleAsync(userSession, botClient, update, cancellationToken);
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
