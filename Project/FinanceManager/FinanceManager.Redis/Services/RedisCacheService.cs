using System.Text.Json;
using FinanceManager.Redis.Services.Interfaces;
using StackExchange.Redis;

namespace FinanceManager.Redis.Services;
public class RedisCacheService : IRedisCacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task SaveData<T>(string key, T value, TimeSpan? expiry = null)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var serializedValue = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetData<T>(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = await db.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value) : default;
    }
}