namespace FinanceManager.Bot.Models;
public sealed class BotCommand
{
    public string Name { get; init; }
    public string Description { get; init; }

    public BotCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
