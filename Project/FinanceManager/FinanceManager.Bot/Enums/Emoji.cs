﻿namespace FinanceManager.Bot.Enums;
public enum Emoji
{
    Greeting,
    Error,
    Success,
    Warning,
    Info,
    Money,
    Remove,
    Category,
    Expense,
    ExpenseAmount,
    Income,
    IncomeAmount,
    Settings,
    History,
    Calendar,
    Skip,
    Rocket,
    Clock,
    Accounts,
    Add,
    Delete,
    Change
}

public static class EmojiExtension
{
    public static string GetSymbol(this Emoji emoji) => emoji switch
    {
        Emoji.Greeting => "👋",
        Emoji.Error => "❌",
        Emoji.Success => "✅",
        Emoji.Warning => "⚠️",
        Emoji.Info => "ℹ️",
        Emoji.Money => "💰",
        Emoji.Add => "➕",
        Emoji.Delete => "🗑️",
        Emoji.Change => "📝",
        Emoji.Remove => "➖",
        Emoji.Category => "🗂️",
        Emoji.Expense => "💸",
        Emoji.ExpenseAmount => "💸",
        Emoji.Income => "💵",
        Emoji.IncomeAmount => "💰",
        Emoji.Settings => "⚙️",
        Emoji.History => "📖",
        Emoji.Calendar => "🗓",
        Emoji.Skip => "➡️",
        Emoji.Rocket => "🚀",
        Emoji.Clock => "🕐",
        Emoji.Accounts => "🏦",
        _ => string.Empty
    };
}