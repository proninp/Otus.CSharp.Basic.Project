namespace FinanceManager.Bot.Services.Abstractions;
public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}
