using FinanceManager.Bot.Models;
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
    private readonly IChatProvider _chatProvider;
    private readonly IUserSessionStateManager _sessionStateManager;

    public BotStateManager(
        IUserSessionProvider userSessionProvider,
        IStateHandlerFactory stateHandlerFactory,
        IChatProvider chatProvider,
        IUserSessionStateManager sessionStateManager)
    {
        _userSessionProvider = userSessionProvider;
        _stateHandlerFactory = stateHandlerFactory;
        _chatProvider = chatProvider;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient, Update update, User? user, CancellationToken cancellationToken)
    {
        var session = await _userSessionProvider.GetUserSession(user, cancellationToken);
        
        if (!_chatProvider.GetChat(session.Id, update, out var chat))
            return;

        var botContext = new BotUpdateContext(session, botClient, update, chat, cancellationToken);

        do
        {
            var handler = _stateHandlerFactory.GetHandler(session.State);
            await handler.HandleAsync(botContext);
        } while (_sessionStateManager.IsContinue(session));
    }
}