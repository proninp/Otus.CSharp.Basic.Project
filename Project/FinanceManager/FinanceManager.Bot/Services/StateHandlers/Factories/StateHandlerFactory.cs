using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Exceptions;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageAccounts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageTransactions;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Bot.Services.StateHandlers.Factories;
public sealed class StateHandlerFactory : IStateHandlerFactory
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

        WorkflowState.CreateAccountStart => _serviceProvider.GetRequiredService<CreateAccountStartStateHandler>(),
        WorkflowState.ChooseAccountName => _serviceProvider.GetRequiredService<ChooseAccountNameStateHandler>(),
        WorkflowState.SendCurrencies => _serviceProvider.GetRequiredService<SendCurrenciesStateHandler>(),
        WorkflowState.ChooseCurrency => _serviceProvider.GetRequiredService<ChooseCurrencyStateHandler>(),
        WorkflowState.SendInputAccountInitialBalance => _serviceProvider.GetRequiredService<SendInputAccountBalanceStateHandler>(),
        WorkflowState.SetAccountInitialBalance => _serviceProvider.GetRequiredService<SetAccountBalanceStateHandler>(),
        WorkflowState.CreateAccountEnd => _serviceProvider.GetRequiredService<CreateAccountEndStateHandler>(),

        WorkflowState.AddExpense => _serviceProvider.GetRequiredService<RegisterExpenseStartStateHandler>(),
        WorkflowState.AddIncome => _serviceProvider.GetRequiredService<RegisterIncomeStartStateHandler>(),

        WorkflowState.SendTransactionCategories => _serviceProvider.GetRequiredService<SendCategoriesStateHandler>(),
        WorkflowState.ChooseTransactionCategory => _serviceProvider.GetRequiredService<ChooseCategoryStateHandler>(),
        WorkflowState.SendInputTransactionDate => _serviceProvider.GetRequiredService<TransactionInputDateStateHandler>(),
        WorkflowState.SetTransactionDate => _serviceProvider.GetRequiredService<TransactionSetDateStateHandler>(),
        WorkflowState.SendInputTransactionAmount => _serviceProvider.GetRequiredService<TransactionIputAmountStateHandler>(),
        WorkflowState.SetTransactionAmount => _serviceProvider.GetRequiredService<TransactionSetAmountStateHandler>(),
        WorkflowState.RegisterTransaction => _serviceProvider.GetRequiredService<TransactionRegistrationStateHandler>(),

        WorkflowState.CreateSettingsMenu => _serviceProvider.GetRequiredService<CreateSettingsMenuStateHandler>(),
        WorkflowState.SelectSettingsMenu => _serviceProvider.GetRequiredService<SelectSettingsMenuStateHandler>(),
        WorkflowState.ManageCategories => _serviceProvider.GetRequiredService<ManageCategoriesStateHandler>(),
        WorkflowState.ManageTransactions => _serviceProvider.GetRequiredService<ManageTransactionsStateHandler>(),
        WorkflowState.ManageAccounts => _serviceProvider.GetRequiredService<ManageAccountsStateHandler>(),

        WorkflowState.CreateManageCategoriesMenu => _serviceProvider.GetRequiredService<CreateManageCategoriesMenuStateHandler>(),
        WorkflowState.SelectManageCategoriesMenu => _serviceProvider.GetRequiredService<SelectManageCategoriesMenuStateHandler>(),


        _ => throw new StateHandlerNotFoundException(state)
    };
}