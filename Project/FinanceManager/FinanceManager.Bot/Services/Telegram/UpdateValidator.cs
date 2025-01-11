using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces.Validators;
using Serilog;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram;
public class UpdateValidator : IUpdateValidator
{
    private readonly ILogger _logger;

    public UpdateValidator(ILogger logger)
    {
        _logger = logger;
    }

    public bool Validate(Update update, [NotNullWhen(true)] out User? user)
    {
        user = update switch
        {
            { Message: { Text: var text, From: var from } } when !string.IsNullOrEmpty(text) => from,
            { EditedMessage: { Text: var text, From: var from } } when !string.IsNullOrEmpty(text) => from,
            { CallbackQuery: { Data: var data, From: var from } } when !string.IsNullOrEmpty(data) => from,
            _ => null
        };

        if (user is null)
            UnknownTypeUpdate(update);

        return user is not null;
    }

    private bool UnknownTypeUpdate(Update update)
    {
        _logger.Information($"Unknown update type: {update.Type}");
        return false;
    }
}