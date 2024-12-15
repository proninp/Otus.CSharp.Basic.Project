using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.CommandHandlers;
public static class BotCommands
{
    public static readonly List<BotCommand> Commands = new()
    {
        new BotCommand("/start", "Начать работу с ботом"),
        new BotCommand("/addAccount", "Добавить счет"),
        new BotCommand("/deleteAccount", "Удалить счет"),
        new BotCommand("/removeaccount", "Удалить счет"),
        new BotCommand("/addCategory", "Добавить категорию"),
        new BotCommand("/removeCategory", "Удалить категорию"),
        new BotCommand("/addExpense", "Добавить расход"),
        new BotCommand("/deleteExpense", "Удалить расход"),
        new BotCommand("/addIncome", "Добавить доход"),
        new BotCommand("/deleteIncome", "Удалить доход"),
        new BotCommand("/addTransfer", "Добавить перевод"),
        new BotCommand("/deleteTransfer", "Удалить перевод"),
    };
}