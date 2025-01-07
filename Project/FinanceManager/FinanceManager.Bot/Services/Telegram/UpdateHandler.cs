using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram;
public class UpdateHandler : IUpdateHandler
{
    private readonly IUpdateValidator _validator;
    private readonly IBotStateManager _botStateManager;
    private readonly ILogger _logger;

    public UpdateHandler(IUpdateValidator validator, IBotStateManager botStateManager, ILogger logger)
    {
        _validator = validator;
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

        if (!_validator.Validate(update, out var user))
            return;

        await OnUpdate(botClient, update, user, cancellationToken);
    }

    private async Task OnUpdate(ITelegramBotClient botClient, Update update, User user, CancellationToken cancellationToken)
    {
        _logger.Debug($"Receive an update with type: {update.Type}");

        await _botStateManager.HandleUpdateAsync(botClient, update, user, cancellationToken);
    }
}
