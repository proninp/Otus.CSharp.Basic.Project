using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class CreateAccountStateHandler : IStateHandler
{
    private readonly ISubStateHandlerFactory _subStateHandlerFactory;

    public CreateAccountStateHandler(ISubStateHandlerFactory subStateHandlerFactory)
    {
        _subStateHandlerFactory = subStateHandlerFactory;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession,
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var subStateHandler = _subStateHandlerFactory.GetSubStateHandler(userSession.SubState);
        userSession.SubState = await subStateHandler.HandleAsync(userSession, botClient, message, cancellationToken);
        if (userSession.SubState == default)
            userSession.UserState = UserState.Default;
        return userSession.UserState;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}