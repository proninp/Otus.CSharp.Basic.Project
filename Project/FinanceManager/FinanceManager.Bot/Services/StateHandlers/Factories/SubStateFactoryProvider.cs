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

    public ISubStateFactory GetSubStateFactory(WorkflowState state) => state switch
    {
        WorkflowState.AddAccount => _serviceProvider.GetRequiredService<CreateAccountSubStateFactory>(),
        WorkflowState.AddExpense => _serviceProvider.GetRequiredService<AddTransactionSubStateFactory>(),
        WorkflowState.AddIncome => _serviceProvider.GetRequiredService<AddTransactionSubStateFactory>(),
        _ => throw new InvalidOperationException($"There is no substate factory provider for the state {state}")
    };
}