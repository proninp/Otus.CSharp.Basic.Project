using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services;
public class BotStateMachine : IBotStateManager
{
    private readonly IUserSessionProvider _userSessionProvider;

    public BotStateMachine(IUserSessionProvider userSessionProvider)
    {
        _userSessionProvider = userSessionProvider;
    }

    public void HandleMessage(Message message)
    {
        var session = _userSessionProvider.GetUserSession(message.From);

        throw new NotImplementedException();
    }
}
