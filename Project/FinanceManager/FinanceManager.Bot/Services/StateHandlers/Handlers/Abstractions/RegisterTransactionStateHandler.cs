using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

public abstract class RegisterTransactionStateHandler : IStateHandler
{
    public Task HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        AddExpenseContext(session);
        
        session.Continue(Enums.WorkflowState.SendTransactionCategories);
        return Task.CompletedTask;
    }

    private protected abstract void AddExpenseContext(UserSession session);
}