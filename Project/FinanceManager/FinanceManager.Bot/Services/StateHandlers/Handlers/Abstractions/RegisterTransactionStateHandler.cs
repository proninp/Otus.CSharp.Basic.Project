using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

public abstract class RegisterTransactionStateHandler : IStateHandler
{
    private readonly StateHandler _stateHandler;

    protected RegisterTransactionStateHandler(StateHandler stateHandler)
    {
        _stateHandler = stateHandler;
    }

    public async Task HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        AddExpenseContext(session);

        await _stateHandler.HandleAsync(session, botClient, update, cancellationToken);
    }

    private protected abstract void AddExpenseContext(UserSession session);
}