using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

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
        _ => throw new InvalidOperationException($"There is no substate handler for the substate {userSubState}")
    };
}
