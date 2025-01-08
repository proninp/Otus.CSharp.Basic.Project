using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public class StateHandlerFactory : IStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StateHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStateHandler GetHandler(UserState userState) => userState.State switch
    {
        WorkflowState.Default => _serviceProvider.GetRequiredService<DefaultStateHandler>(),
        WorkflowState.Menu => _serviceProvider.GetRequiredService<MenuStateHandler>(),
        WorkflowState.AddExpense => _serviceProvider.GetRequiredService<RegisterExpenseStateHandler>(),
        WorkflowState.AddIncome => _serviceProvider.GetRequiredService<RegisterIncomeStateHandler>(),
        WorkflowState.AddAccount => _serviceProvider.GetRequiredService<CreateAccountStateHandler>(),
        _ => throw new InvalidOperationException($"There is no handler for the state {userState}")
    };
}
