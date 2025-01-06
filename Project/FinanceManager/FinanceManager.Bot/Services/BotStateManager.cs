using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
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

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, User? user, CancellationToken cancellationToken)
    {
        var session = await _userSessionProvider.GetUserSession(user, cancellationToken);
        while (true)
        {
            var handler = _stateHandlerFactory.GetHandler(session.UserState);
            var nextState = await handler.HandleStateAsync(session, botClient, update, cancellationToken);
            if (nextState is null || nextState == session.UserState)
                break;
            session.UserState = nextState.Value;
        }
    }
}