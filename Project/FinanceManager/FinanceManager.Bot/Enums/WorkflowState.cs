namespace FinanceManager.Bot.Enums;
public enum WorkflowState
{
    Default,
    CreateMenu,
    SelectMenu,
    AddAccount,
    CreateAccountStart,
    ChooseAccountName,
    SendCurrencies,
    ChooseCurrency,
    SetAccountInitialBalance,
    CreateAccountEnd,

    DeleteAccount,
    AddCategory,
    DeleteCategory,
    AddExpense,
    DeleteExpense,
    AddIncome,
    DeleteIncome,
    
    SendTransactionDateSelection,
    SetTransactionDate,
    SetTransactionAmount,
    SendTransactionCategories,
    ChooseTransactionCategory,
    RegisterTransaction,

    AddTransfer,
    DeleteTransfer,
    History,
    Settings
}
