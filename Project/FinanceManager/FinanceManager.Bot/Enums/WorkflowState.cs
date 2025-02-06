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
    ManageCategories,
    ManageTransactions,
    ManageAccounts,

    CreateManageCategoriesMenu,
    SelectManageCategoriesMenu,

    SendChooseNewCategoryType,
    SetNewCategoryType,
    SendInputNewCategoryName,
    SetNewCategoryName,
    SendInputNewCategoryEmoji,
    SetNewCategoryEmoji,
    RegisterNewCategory,

    SendChooseDeletingCategoryType,
    SetDeletingCategoryType,
    SendChooseCategoryToDelete,
    ChooseCategoryToDelete,
    SendDeletingCategoryConfirmation,
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
