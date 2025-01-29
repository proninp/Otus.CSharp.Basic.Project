using System.Text.Json;
using FinanceManager.Redis.Services.Interfaces;
using StackExchange.Redis;

namespace FinanceManager.Redis.Services;
public class RedisCacheService : IRedisCacheService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task SaveData<T>(string key, T value, TimeSpan? expiry = null)
    {
        var db = _redis.GetDatabase();
        var serializedValue = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetData<T>(string key)
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);
        
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return value.HasValue ? JsonSerializer.Deserialize<T>(value, options) : default;
    }
}