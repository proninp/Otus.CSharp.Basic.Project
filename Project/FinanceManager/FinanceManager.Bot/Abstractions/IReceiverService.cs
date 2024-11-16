namespace FinanceManager.Bot.Abstractions;
public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}
