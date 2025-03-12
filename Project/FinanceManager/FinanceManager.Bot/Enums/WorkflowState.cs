namespace FinanceManager.Bot.Enums;
public enum WorkflowState
{
    Default,
    CreateMenu,
    SelectMenu,
    CreateAccountStart,
    ChooseAccountName,
    SendCurrencies,
    ChooseCurrency,
    SendInputAccountInitialBalance,
    SetAccountInitialBalance,
    CreateAccountComplete,

    AddExpense,
    AddIncome,
    
    SendInputTransactionDate,
    SetTransactionDate,
    SendInputTransactionAmount,
    SetTransactionAmount,
    SendTransactionCategories,
    ChooseTransactionCategory,
    AddTransactionComplete,

    CreateSettingsMenu,
    SelectSettingsMenu,
    ManageTransactions,
    ManageAccounts,

    CreateManageCategoriesMenu,
    SelectManageCategoriesMenu,

    SendNewCategoryType,
    SetNewCategoryType,
    SendInputNewCategoryName,
    SetNewCategoryName,
    SendInputNewCategoryEmoji,
    SetNewCategoryEmoji,
    NewCategoryComplete,

    SendChooseDeleteCategoryType,
    SetDeleteCategoryType,
    SendChooseCategoryToDelete,
    ChooseCategoryToDelete,
    SendDeleteCategoryConfirmation,
    HandleDeletingCategoryConfirmation,
    DeleteCategoryComplete,

    SendChooseRenamingCategoryType,
    SetRenamingCategoryType,
    SendChooseRenamingCategory,
    ChooseCategoryToRename,
    SendInputCategoryName,
    SetCategoryName,
    SendInputCategoryEmoji,
    SetCategoryEmoji,
    RegisterRenameCategory,

    History,
}
