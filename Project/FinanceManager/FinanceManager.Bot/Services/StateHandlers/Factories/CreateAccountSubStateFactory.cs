using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;

public class CreateAccountSubStateFactory : ISubStateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CreateAccountSubStateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStateHandler GetSubStateHandler(UserState userState) => userState.SubState switch
    {
        WorkflowSubState.Default => _serviceProvider.GetRequiredService<CreateAccountDefaultSubStateHandler>(),
        WorkflowSubState.ChooseAccountName => _serviceProvider.GetRequiredService<ChooseAccountNameSubStateHandler>(),
        WorkflowSubState.SendCurrencies => _serviceProvider.GetRequiredService<SendCurrenciesSubStateHandler>(),
        WorkflowSubState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencySubStateHandler>(),
        WorkflowSubState.SetAccountInitialBalance => _serviceProvider.GetRequiredService<SetAccountBalanceSubStateHandler>(),
        WorkflowSubState.Complete => _serviceProvider.GetRequiredService<CreateAccountCompleteSubStateHandler>(),
        _ => throw new SubStateHandlerNotFoundException(userState.State, userState.SubState)
    };
}