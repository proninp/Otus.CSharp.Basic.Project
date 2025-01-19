using System.ComponentModel;

namespace FinanceManager.Bot.Enums;
public enum HistoryCommand
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
    public static string GetSymbol(this HistoryCommand command) => command switch
    {
        HistoryCommand.Newer => "◀️",
        HistoryCommand.Older => "▶️",
        HistoryCommand.Memu => "↩️",
        _ => string.Empty
    };
}