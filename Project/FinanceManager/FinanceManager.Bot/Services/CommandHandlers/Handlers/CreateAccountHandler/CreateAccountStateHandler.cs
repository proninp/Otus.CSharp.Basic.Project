using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class CreateAccountStateHandler : IStateHandler
{
    private readonly ISubStateHandlerFactory _subStateHandlerFactory;
    private readonly IAccountManager _accountManager;

    public CreateAccountStateHandler(ISubStateHandlerFactory subStateHandlerFactory, IAccountManager accountManager)
    {
        _subStateHandlerFactory = subStateHandlerFactory;
        _accountManager = accountManager;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession,
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var subStateHandler = _subStateHandlerFactory.GetSubStateHandler(userSession.SubState);
        userSession.SubState = await subStateHandler.HandleAsync(userSession, botClient, message, cancellationToken);
        if (userSession.SubState == UserSubState.Complete)
        {
            CreateAccount(userSession, cancellationToken);
            userSession.ResetState();
        }
        return userSession.UserState;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void CreateAccount(UserSession userSession, CancellationToken cancellationToken)
    {
        var context = userSession.GetCreateAccountContext();
        throw new NotImplementedException();
    }
}