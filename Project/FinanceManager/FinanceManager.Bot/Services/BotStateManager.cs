using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services;
public class BotStateManager : IBotStateManager
{
    private readonly IUserSessionProvider _userSessionProvider;
    private readonly IStateHandlerFactory _stateHandlerFactory;

    public BotStateManager(IUserSessionProvider userSessionProvider, IStateHandlerFactory stateHandlerFactory)
    {
        _userSessionProvider = userSessionProvider;
        _stateHandlerFactory = stateHandlerFactory;
    }

    public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var session = await _userSessionProvider.GetUserSession(message.From, cancellationToken);

        while (true)
        {
            var handler = _stateHandlerFactory.GetHandlerAsync(session.UserState);
            var nextState = await handler.HandleStateAsync(session, message, cancellationToken);
            if (nextState is null)
                break;
            session.UserState = nextState.Value;
        }
    }
}