namespace FinanceManager.Redis.Services.Interfaces;
public interface IRedisCacheService
{
    Task SaveData<T>(string key, T value, TimeSpan? expiry = null);

    Task<T?> GetData<T>(string key);
}