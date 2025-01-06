using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public class SubStateFactoryProvider : ISubStateFactoryProvider
{
    private readonly IServiceProvider _serviceProvider;

    public SubStateFactoryProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISubStateFactory GetSubStateFactory(UserState state) => state switch
    {
        UserState.AddAccount => _serviceProvider.GetRequiredService<CreateAccountSubStateFactory>(),
        UserState.AddExpense => _serviceProvider.GetRequiredService<AddExpenseSubStateFactory>(),
        _ => throw new InvalidOperationException($"There is no substate factory provider for the state {state}")
    };
}