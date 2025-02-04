using System.ComponentModel;

namespace FinanceManager.Bot.Enums;
public enum NavigationCommand
{
    [Description("Newer")]
    Newer,
    [Description("Older")]
    Older,
    [Description("Menu")]
    Memu
}

public static class HistoryCommandExtension
{
    public static string GetSymbol(this NavigationCommand command) => command switch
    {
        NavigationCommand.Newer => "◀️",
        NavigationCommand.Older => "▶️",
        NavigationCommand.Memu => "↩️",
        _ => string.Empty
    };

    public static string GetCallbackData(this NavigationCommand command) => 
        $"{nameof(NavigationCommand)}_{command.ToString()}";
}