namespace FinanceManager.Bot.Enums;
public enum WorkflowSubState
{
    Default,
    ChooseAccountName,
    SendCurrencies,
    ChooseCurrency,
    SetAccountInitialBalance,
    SelectMenu,
    SendTransactionDateSelection,
    SetTransactionDate,
    SetTransactionAmount,
    ChooseTransactionCategory,
    RegisterTransaction,
    Complete
}