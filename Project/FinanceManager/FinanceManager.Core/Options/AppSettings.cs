namespace FinanceManager.Core.Options;
public sealed class AppSettings
{
    public required string DbConnectionString { get; set; }

    public required string BotToken { get; set; }
}
