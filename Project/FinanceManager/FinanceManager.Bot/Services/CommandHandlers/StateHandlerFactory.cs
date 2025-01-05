using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.CommandHandlers;
public class StateHandlerFactory : IStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StateHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStateHandler GetHandler(UserState userState) => userState switch
    {
        UserState.Default => _serviceProvider.GetRequiredService<DefaultStateHandler>(),
        UserState.Menu => _serviceProvider.GetRequiredService<MenuStateHandler>(),
        UserState.AddExpense => _serviceProvider.GetRequiredService<RegisterExpenseStateHandler>(),
        UserState.AddIncome => _serviceProvider.GetRequiredService<RegisterIncomeStateHandler>(),
        UserState.AddAccount => _serviceProvider.GetRequiredService<CreateAccountStateHandler>(),
        _ => throw new InvalidOperationException($"There is no handler for the state {userState}")
    };
}
