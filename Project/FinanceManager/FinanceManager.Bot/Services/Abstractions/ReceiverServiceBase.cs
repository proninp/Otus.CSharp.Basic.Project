using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace FinanceManager.Bot.Services.Abstractions;
public abstract class ReceiverServiceBase<TUpdateHandler> : IReceiverService
    where TUpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUpdateHandler _updateHandler;
    private readonly ILogger _logger;

    public ReceiverServiceBase(
        ITelegramBotClient botClient,
        TUpdateHandler updateHandler,
        ILogger logger)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
        _logger = logger;
    }

    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = [],
            DropPendingUpdates = true,
        };

        var me = await _botClient.GetMe(stoppingToken);
        _logger.Information($"Начали получать обновления от {me.Username}");

        await _botClient.ReceiveAsync(
            updateHandler: _updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken);
    }
}
