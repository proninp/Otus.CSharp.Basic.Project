using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces.Providers;
using Serilog;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public sealed class ChatProvider : IChatProvider
{
    private readonly ILogger _logger;

    public ChatProvider(ILogger logger)
    {
        _logger = logger;
    }

    public bool GetChat(Guid userId, Update update, [NotNullWhen(true)] out Chat? chat)
    {
        chat = update switch
        {
            { Message: { Chat: var messageChat } } => messageChat,
            { EditedMessage: { Chat: var editMessageChat } } => editMessageChat,
            { CallbackQuery: { Message: { Chat: var callbackChat } } } => callbackChat,
            _ => null
        };

        if (chat is null)
        {
            _logger.Warning($"The chat for the new update with type {update.Type} could not be found for the user {userId}.");
            return false;
        }

        return true;
    }
}