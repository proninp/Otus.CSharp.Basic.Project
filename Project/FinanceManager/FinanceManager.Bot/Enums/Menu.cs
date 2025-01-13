using System.ComponentModel;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Services.CommandHandlers.Handlers;

namespace FinanceManager.Bot.Enums;
public enum Menu
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
    public static string GetKey(this Menu menu) => menu switch
    {
        Menu.Expense => nameof(RegisterExpenseStartStateHandler),
        Menu.Income => nameof(RegisterIncomeStartStateHandler),
        Menu.History => nameof(HistoryStateHandler),
        Menu.Settings => nameof(SettingsStateHandler),
        _ => throw new NotImplementedException(menu.GetDescription())
    };
}
