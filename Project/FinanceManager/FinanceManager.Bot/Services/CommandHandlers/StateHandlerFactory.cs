using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.CommandHandlers;
public class StateHandlerFactory : IStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StateHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandStateHandler GetHandler(UserState userState) => userState switch
    {
        UserState.Start => _serviceProvider.GetRequiredService<StartStateHandler>(),
        UserState.RegisterExpense => _serviceProvider.GetRequiredService<RegisterExpenseStateHandler>(),
        UserState.RegisterIncome => _serviceProvider.GetRequiredService<RegisterIncomeStateHandler>(),
        UserState.CreateAccount => _serviceProvider.GetRequiredService<CreateAccountStateHandler>(),
        UserState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencyStateHandler>(),
        UserState.ChooseAccountType => _serviceProvider.GetRequiredService<ChooseAccountTypeStateHandler>(),
        UserState.SetInitialBalance => _serviceProvider.GetRequiredService<SetInitialBalanceStateHandler>(),
            _ => throw new InvalidOperationException($"Нет обработчика для состояния {userState}")
    };
}
