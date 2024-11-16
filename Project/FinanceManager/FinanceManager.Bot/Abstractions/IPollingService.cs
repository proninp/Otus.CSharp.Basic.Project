namespace FinanceManager.Bot.Abstractions;
public interface IPollingService
{
    Task DoWork(CancellationToken stoppingToken);
}
