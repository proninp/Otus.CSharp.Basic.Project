using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;

public class CreateAccountSubStateFactory : IAccountSubStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CreateAccountSubStateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISubStateHandler GetSubStateHandler(UserSubState userSubState) => userSubState switch
    {
        UserSubState.Default => _serviceProvider.GetRequiredService<CreateAccountDefaultSubStateHandler>(),
        UserSubState.ChooseAccountName => _serviceProvider.GetRequiredService<ChooseAccountNameSubStateHandler>(),
        UserSubState.SendCurrencies => _serviceProvider.GetRequiredService<SendCurrenciesSubStateHandler>(),
        UserSubState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencySubStateHandler>(),
        UserSubState.SetAccountInitialBalance => _serviceProvider.GetRequiredService<SetAccountBalanceSubStateHandler>(),
        _ => throw new InvalidOperationException($"There is no substate handler for the substate {userSubState}")
    };
}