using FinanceManager.Redis.Services.Interfaces;
using StackExchange.Redis;

namespace FinanceManager.Redis.Services;
public class RedisInitializer : IRedisInitializer
{
    public readonly IConnectionMultiplexer _redis;

    public RedisInitializer(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task ClearDatabase()
    {
        var db = _redis.GetDatabase();
        await db.ExecuteAsync("FLUSHDB");
    }
}
