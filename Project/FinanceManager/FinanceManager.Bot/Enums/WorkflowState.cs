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
    CreateAccountEnd,

    AddExpense,
    AddIncome,
    
    SendInputTransactionDate,
    SetTransactionDate,
    SendInputTransactionAmount,
    SetTransactionAmount,
    SendTransactionCategories,
    ChooseTransactionCategory,
    RegisterTransaction,

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
    RegisterNewCategory,

    SendChooseDeleteCategoryType,
    SetDeleteCategoryType,
    SendChooseCategoryToDelete,
    ChooseCategoryToDelete,
    SendDeleteCategoryConfirmation,
    HandleDeletingCategoryConfirmation,
    RegisterDeleteCategory,

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
