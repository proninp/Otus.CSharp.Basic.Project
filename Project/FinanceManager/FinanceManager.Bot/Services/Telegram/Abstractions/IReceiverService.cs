namespace FinanceManager.Bot.Services.Telegram.Abstractions;
public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}
