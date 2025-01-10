using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class RegisterTransactionStateHandler : IStateHandler
{
    private readonly ISubStateFactoryProvider _subStateFactoryProvider;

    protected RegisterTransactionStateHandler(ISubStateFactoryProvider subStateFactoryProvider)
    {
        _subStateFactoryProvider = subStateFactoryProvider;
    }

    public async Task HandleStateAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        AddExpenseContext(session);

        var subStateHandlerFactory = _subStateFactoryProvider.GetSubStateFactory(session.State);
        var subStateHandler = subStateHandlerFactory.GetSubStateHandler(session.SubState);

        await subStateHandler.HandleAsync(session, botClient, update, cancellationToken);

        if (session.SubState == WorkflowSubState.Complete)
            session.ResetState();
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private protected abstract void AddExpenseContext(UserSession session);
}