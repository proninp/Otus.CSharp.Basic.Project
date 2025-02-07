using System.ComponentModel;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageAccounts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageTransactions;

namespace FinanceManager.Bot.Enums;
public enum SettingsMenu
{
    [Description("None")]
    None,
    [Description("Manage Categories")]
    ManageCategories,
    [Description("Manage Transactions")]
    ManageTransactios,
    [Description("Manage Accounts")]
    ManageAccounts,
    [Description("Export Transactions")]
    ExportTransactions
}

public static class SettingsMenuExtension
{
    public static string GetKey(this SettingsMenu settingsMenu) => settingsMenu switch
    {
        SettingsMenu.ManageCategories => nameof(ManageCategoriesStateHandler),
        SettingsMenu.ManageTransactios => nameof(ManageTransactionsStateHandler),
        SettingsMenu.ManageAccounts => nameof(ManageAccountsStateHandler),
        _ => throw new NotImplementedException(settingsMenu.GetDescription())
    };
}