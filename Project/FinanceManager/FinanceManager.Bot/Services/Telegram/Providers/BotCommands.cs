using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public static class BotCommands
{
    public static readonly List<BotCommand> Commands = new()
    {
        new BotCommand("/start", "Getting started with the bot"),
        new BotCommand("/addAccount", "Add account"),
        new BotCommand("/removeAccount", "Remove account"),
        new BotCommand("/addCategory", "Add category"),
        new BotCommand("/removeCategory", "Remove category"),
        new BotCommand("/addExpense", "Register expense"),
        new BotCommand("/removeExpense", "Remove Expense"),
        new BotCommand("/addIncome", "Register income"),
        new BotCommand("/removeIncome", "Remove income"),
        new BotCommand("/addTransfer", "Register transfer"),
        new BotCommand("/removeTransfer", "Remove transfer"),
    };
}