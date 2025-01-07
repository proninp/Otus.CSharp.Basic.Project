using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public class AddExpenseSubStateFactory : ISubStateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AddExpenseSubStateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISubStateHandler GetSubStateHandler(UserSubState userSubState) => userSubState switch
    {
        UserSubState.Default => _serviceProvider.GetRequiredService<TransactionDefaultStateHandler>(),
        UserSubState.SetExpenseDate => _serviceProvider.GetRequiredService<TransactionSetDateStateHandler>(),
        _ => throw new InvalidOperationException($"There is no substate handler for the substate {userSubState}")
    };
}
