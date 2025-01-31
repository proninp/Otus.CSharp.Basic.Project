using FinanceManager.Bot.Enums;
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

    public Task HandleAsync(BotUpdateContext updateContext)
    {
        AddExpenseContext(updateContext.Session);

        _sessionStateManager.Continue(updateContext.Session, WorkflowState.SendTransactionCategories);

        return Task.CompletedTask;
    }

    private protected abstract void AddExpenseContext(UserSession session);
}