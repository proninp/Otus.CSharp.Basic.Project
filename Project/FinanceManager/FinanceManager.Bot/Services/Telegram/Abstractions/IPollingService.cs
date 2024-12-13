namespace FinanceManager.Bot.Services.Telegram.Abstractions;
public interface IPollingService
{
    Task DoWork(CancellationToken stoppingToken);
}
