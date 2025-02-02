namespace FinanceManager.Redis.Services.Interfaces;
public interface IRedisCacheService
{
    Task SaveDataAsync<T>(string key, T value, TimeSpan? expiry = null);

    void SaveData<T>(string key, T value, TimeSpan? expiry = null);

    Task<T?> GetDataAsync<T>(string key);
}