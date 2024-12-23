using FinanceManager.Bot.Services.Interfaces;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram;
public class UpdateHandler : IUpdateHandler
{
    private readonly IBotStateManager _botStateManager;
    private readonly ILogger _logger;

    public UpdateHandler(IBotStateManager botStateManager, ILogger logger)
    {
        _botStateManager = botStateManager;
        _logger = logger;
    }

    public async Task HandleErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        _logger.Information("HandleError: {Exception}", exception);

        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await (update switch
        {
            { Message: { } message } => OnMessage(botClient, message, cancellationToken),
            { EditedMessage: { } message } => OnMessage(botClient, message, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update)
        });
    }

    private async Task OnMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        _logger.Debug("Receive message type: {MessageType}", message.Type);
        if (message.Text is not { } messageText)
            return;

        await _botStateManager.HandleMessageAsync(botClient, message, cancellationToken);
    }

    private Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.Information("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}
