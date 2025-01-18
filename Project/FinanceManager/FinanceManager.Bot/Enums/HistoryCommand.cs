using System.ComponentModel;

namespace FinanceManager.Bot.Enums;
public enum HistoryCommand
{
    [Description("Older")]
    Next,
    [Description("Newer")]
    Previous,
    [Description("Menu")]
    Memu
}

public static class HistoryCommandExtension
{
    public static string GetSymbol(this HistoryCommand command) => command switch
    {
        HistoryCommand.Next => "▶️",
        HistoryCommand.Previous => "◀️",
        HistoryCommand.Memu => "↩️",
        _ => string.Empty
    };
}