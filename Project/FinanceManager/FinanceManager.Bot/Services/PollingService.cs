using FinanceManager.Bot.Services.Abstractions;
using Serilog;

namespace FinanceManager.Bot.Services;
public class PollingService(IReceiverService receiverService, ILogger logger) : IPollingService
{
    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await receiverService.ReceiveAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.Error("Поллинг завершился с ошибкой: {Exception}", ex);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Cooldown if something goes wrong
            }
        }
    }
}
