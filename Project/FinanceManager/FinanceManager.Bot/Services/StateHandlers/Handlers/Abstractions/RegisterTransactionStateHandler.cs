using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

public abstract class RegisterTransactionStateHandler : IStateHandler
{
    private readonly ISessionStateManager _sessionStateManager;

    protected RegisterTransactionStateHandler(ISessionStateManager sessionStateManager)
    {
        _sessionStateManager = sessionStateManager;
    }

    public Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        AddExpenseContext(updateContext.Session);

        var result = _sessionStateManager.Next(updateContext.Session);

        return Task.FromResult(result);
    }

    private protected abstract void AddExpenseContext(UserSession session);
}