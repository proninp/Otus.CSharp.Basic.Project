using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

public abstract class RegisterTransactionStateHandler : IStateHandler
{
    public Task HandleAsync(BotUpdateContext updateContext)
    {
        AddExpenseContext(updateContext.Session);

        updateContext.Session.Continue(Enums.WorkflowState.SendTransactionCategories);
        return Task.CompletedTask;
    }

    private protected abstract void AddExpenseContext(UserSession session);
}