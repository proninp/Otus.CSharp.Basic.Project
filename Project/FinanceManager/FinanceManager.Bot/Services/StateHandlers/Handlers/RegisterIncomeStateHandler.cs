using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class RegisterIncomeStateHandler : RegisterTransactionStateHandler
{
    public RegisterIncomeStateHandler(StateHandler stateHandler)
        : base(stateHandler) { }

    private protected override void AddExpenseContext(UserSession session)
    {
        if (session.ContextData is null)
        {
            session.SetData(new TransactionContext { TransactionType = TransactionType.Income });
        }
    }
}