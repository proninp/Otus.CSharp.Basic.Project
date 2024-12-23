using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
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

    public async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var session = await _userSessionProvider.GetUserSession(message.From, cancellationToken);

        while (true)
        {
            var handler = _stateHandlerFactory.GetHandlerAsync(session.UserState);
            var nextState = await handler.HandleStateAsync(session, botClient, message, cancellationToken);
            if (nextState is null || nextState == session.UserState)
                break;
            session.UserState = nextState.Value;
        }
    }
}