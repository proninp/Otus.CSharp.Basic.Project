using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public class StateHandlerFactory : IStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StateHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStateHandler GetHandler(WorkflowState state) => state switch
    {
        WorkflowState.Default => _serviceProvider.GetRequiredService<DefaultStateHandler>(),
        WorkflowState.CreateMenu => _serviceProvider.GetRequiredService<CreateMenuStateHandler>(),
        WorkflowState.SelectMenu => _serviceProvider.GetRequiredService<SelectMenuStateHandler>(),

        WorkflowState.History => _serviceProvider.GetRequiredService<HistoryStateHandler>(),
        WorkflowState.Settings => _serviceProvider.GetRequiredService<SettingsStateHandler>(),

        WorkflowState.CreateAccountStart => _serviceProvider.GetRequiredService<CreateAccountStartStateHandler>(),
        WorkflowState.ChooseAccountName => _serviceProvider.GetRequiredService<ChooseAccountNameStateHandler>(),
        WorkflowState.SendCurrencies => _serviceProvider.GetRequiredService<SendCurrenciesStateHandler>(),
        WorkflowState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencyStateHandler>(),
        WorkflowState.SetAccountInitialBalance => _serviceProvider.GetRequiredService<SetAccountBalanceStateHandler>(),
        WorkflowState.CreateAccountEnd => _serviceProvider.GetRequiredService<CreateAccountEndStateHandler>(),

        WorkflowState.AddExpense => _serviceProvider.GetRequiredService<RegisterExpenseStartStateHandler>(),
        WorkflowState.AddIncome => _serviceProvider.GetRequiredService<RegisterIncomeStartStateHandler>(),

        WorkflowState.SendTransactionCategories => _serviceProvider.GetRequiredService<SendCategoriesStateHandler>(),
        WorkflowState.ChooseTransactionCategory => _serviceProvider.GetRequiredService<ChooseCategoryStateHandler>(),
        WorkflowState.SendTransactionDateSelection => _serviceProvider.GetRequiredService<TransactionDateSelectionStateHandler>(),
        WorkflowState.SetTransactionDate => _serviceProvider.GetRequiredService<TransactionSetDateStateHandler>(),
        WorkflowState.SetTransactionAmount => _serviceProvider.GetRequiredService<TransactionSetAmountStateHandler>(),
        WorkflowState.RegisterTransaction => _serviceProvider.GetRequiredService<TransactionRegistrationStateHandler>(),
        _ => throw new StateHandlerNotFoundException(state)
    };
}