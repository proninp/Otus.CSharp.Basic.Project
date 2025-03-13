using System.Text.Json;
using FinanceManager.Redis.Services.Interfaces;
using Serilog;
using StackExchange.Redis;

namespace FinanceManager.Redis.Services;
public class RedisCacheService : IRedisCacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger _logger;

    public RedisCacheService(
        IConnectionMultiplexer redis,
        ILogger logger)
    {
        _redis = redis;
        _logger = logger;
    }

    public async Task SaveDataAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var db = _redis.GetDatabase();
        var serializedValue = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, serializedValue, expiry);
    }

    public void SaveData<T>(string key, T value, TimeSpan? expiry = null)
    {
        var db = _redis.GetDatabase();
        var serializedValue = JsonSerializer.Serialize(value);
        db.StringSet(key, serializedValue);
        db.KeyExpire(key, expiry);
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);

        var ttl = await db.KeyTimeToLiveAsync(key);
        _logger.Debug($"TTL for key {key}: {ttl}");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return value.HasValue ? JsonSerializer.Deserialize<T>(value, options) : default;
    }
}