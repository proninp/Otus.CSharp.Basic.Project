﻿namespace FinanceManager.Bot.Enums;
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
    SendInputAccountInitialBalance,
    SetAccountInitialBalance,
    CreateAccountEnd,

    DeleteAccount,
    AddCategory,
    DeleteCategory,
    AddExpense,
    DeleteExpense,
    AddIncome,
    DeleteIncome,
    
    SendInputTransactionDate,
    SetTransactionDate,
    SendInputTransactionAmount,
    SetTransactionAmount,
    SendTransactionCategories,
    ChooseTransactionCategory,
    RegisterTransaction,

    AddTransfer,
    DeleteTransfer,
    History,
    Settings
}
