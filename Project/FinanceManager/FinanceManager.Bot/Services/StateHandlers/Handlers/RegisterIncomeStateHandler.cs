using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class RegisterIncomeStateHandler : RegisterTransactionStateHandler
{
    public RegisterIncomeStateHandler(ISubStateFactoryProvider subStateFactoryProvider)
        : base(subStateFactoryProvider) { }

    private protected override void AddExpenseContext(UserSession session)
    {
        if (session.ContextData is null)
        {
            session.SetData(new TransactionContext { TransactionType = TransactionType.Income });
        }
    }
}