namespace FinanceManager.Core.Options;
public sealed class AppSettings
{
    public required string DbConnectionString { get; set; }
    
    public required string RedisConnectionString { get; set; }
    
    public required string RedisPassword { get; set; }

    public required string BotToken { get; set; }

    public required int RedisUserSessionExpirationMinutes { get; set; }

    public required int InMemoryUserSessionExpirationMinutes { get; set; }
}