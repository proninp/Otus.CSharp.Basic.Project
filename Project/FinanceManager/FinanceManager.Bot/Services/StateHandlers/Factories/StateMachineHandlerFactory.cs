using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandler;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateAccountHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public class StateMachineHandlerFactory : IStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StateMachineHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStateHandler GetHandler(BotState state) => state switch
    {
        BotState.Default => _serviceProvider.GetRequiredService<DefaultStateHandler>(),
        BotState.Menu => _serviceProvider.GetRequiredService<MenuStateHandler>(),
        BotState.CreateAccountStart => _serviceProvider.GetRequiredService<CreateAccountDefaultSubStateHandler>(),
        BotState.ChooseAccountName => _serviceProvider.GetRequiredService<ChooseAccountNameSubStateHandler>(),
        BotState.SendCurrencies => _serviceProvider.GetRequiredService<SendCurrenciesSubStateHandler>(),
        BotState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencySubStateHandler>(),
        BotState.SetAccountBalance => _serviceProvider.GetRequiredService<SetAccountBalanceSubStateHandler>(),
        BotState.CreateAccountEnd => _serviceProvider.GetRequiredService<CreateAccountCompleteSubStateHandler>(),
        BotState.CreateExpense => _serviceProvider.GetRequiredService<RegisterExpenseStateHandler>(),
        BotState.CreateIncome => _serviceProvider.GetRequiredService<RegisterIncomeStateHandler>(),
        BotState.SendCategories => _serviceProvider.GetRequiredService<SendCategoriesSubStateHandler>(),
        BotState.ChooseCategory => _serviceProvider.GetRequiredService<ChooseCategorySubStateHandler>(),
        BotState.TransactionSendDateSelection => _serviceProvider.GetRequiredService<TransactionSendDateSelectionSubStateHandler>(),
        BotState.TransactionSetDate => _serviceProvider.GetRequiredService<TransactionSetDateSubStateHandler>(),
        BotState.TransactionSetAmount => _serviceProvider.GetRequiredService<TransactionSetAmountSubStateHandler>(),
        BotState.TransactionRegistration => _serviceProvider.GetRequiredService<TransactionRegistrationSubStateHandler>(),
        BotState.ShowHistory => _serviceProvider.GetRequiredService<HistoryStateHandler>(),
        BotState.Settings => _serviceProvider.GetRequiredService<SettingsStateHandler>(),
        _ => throw new BotStateHandlerFactoryException(state)
    };
}