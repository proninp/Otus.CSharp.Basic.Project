namespace FinanceManager.Bot.Enums;
public enum Emoji
{
    Error,
    Success,
    Warning,
    Info,
    Money,
    Add,
    Remove,
    Settings,
    History
}

public static class EmojiExtension
{
    public static string GetSymbol(this Emoji emoji) => emoji switch
    {
        Emoji.Error => "❌",
        Emoji.Success => "✅",
        Emoji.Warning => "⚠️",
        Emoji.Info => "ℹ️",
        Emoji.Money => "💰",
        Emoji.Add => "➕",
        Emoji.Remove => "➖",
        Emoji.Settings => "⚙️",
        Emoji.History => "📖",
        _ => string.Empty
    };
}