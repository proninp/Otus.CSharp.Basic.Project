using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;

public class CreateAccountSubStateFactory : ISubStateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CreateAccountSubStateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISubStateHandler GetSubStateHandler(WorkflowSubState userSubState) => userSubState switch
    {
        WorkflowSubState.Default => _serviceProvider.GetRequiredService<CreateAccountDefaultSubStateHandler>(),
        WorkflowSubState.ChooseAccountName => _serviceProvider.GetRequiredService<ChooseAccountNameSubStateHandler>(),
        WorkflowSubState.SendCurrencies => _serviceProvider.GetRequiredService<SendCurrenciesSubStateHandler>(),
        WorkflowSubState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencySubStateHandler>(),
        WorkflowSubState.SetAccountInitialBalance => _serviceProvider.GetRequiredService<SetAccountBalanceSubStateHandler>(),
        _ => throw new InvalidOperationException($"There is no substate handler for the substate {userSubState}")
    };
}