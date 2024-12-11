namespace FinanceManager.Bot.Services.Abstractions;
public interface IPollingService
{
    Task DoWork(CancellationToken stoppingToken);
}
