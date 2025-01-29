namespace FinanceManager.Redis.Services.Interfaces;
public interface IRedisInitializer
{
    Task ClearDatabase();
}
