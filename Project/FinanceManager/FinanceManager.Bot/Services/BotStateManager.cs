using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services;
public sealed class BotStateManager : IBotStateManager
{
    private readonly ISessionProvider _userSessionProvider;
    private readonly IStateHandlerFactory _stateHandlerFactory;
    private readonly IChatProvider _chatProvider;
    private readonly ISessionConsistencyValidator _consistencyValidator;

    public BotStateManager(
        ISessionProvider userSessionProvider,
        IStateHandlerFactory stateHandlerFactory,
        IChatProvider chatProvider,
        ISessionStateManager sessionStateManager,
        ISessionConsistencyValidator consistencyValidator)
    {
        _userSessionProvider = userSessionProvider;
        _stateHandlerFactory = stateHandlerFactory;
        _chatProvider = chatProvider;
        _consistencyValidator = consistencyValidator;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient, Update update, User? user, CancellationToken cancellationToken)
    {
        var session = await _userSessionProvider.GetUserSession(user, cancellationToken);
        
        if (!_chatProvider.GetChat(session.Id, update, out var chat))
            return;

        var botContext = new BotUpdateContext(session, botClient, update, chat, cancellationToken);

        await _consistencyValidator.ValidateCallbackConsistency(botContext);

        bool isContinue;
        do
        {
            var handler = _stateHandlerFactory.GetHandler(session.State);
            isContinue = await handler.HandleAsync(botContext);
        } while (isContinue);
    }
}