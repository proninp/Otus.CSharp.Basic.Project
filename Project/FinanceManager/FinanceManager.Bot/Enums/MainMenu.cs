using System.ComponentModel;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Services.StateHandlers.Handlers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Settings;

namespace FinanceManager.Bot.Enums;
public enum MainMenu
{
    [Description("None")]
    None,
    [Description("Expense")]
    Expense,
    [Description("Income")]
    Income,
    [Description("History")]
    History,
    [Description("Settings")]
    Settings
}

public static class MenuExtension
{
    public static string GetKey(this MainMenu menu) => menu switch
    {
        MainMenu.Expense => nameof(RegisterExpenseStartStateHandler),
        MainMenu.Income => nameof(RegisterIncomeStartStateHandler),
        MainMenu.History => nameof(HistoryStateHandler),
        MainMenu.Settings => nameof(SettingsStateHandler),
        _ => throw new NotImplementedException(menu.GetDescription())
    };
}
