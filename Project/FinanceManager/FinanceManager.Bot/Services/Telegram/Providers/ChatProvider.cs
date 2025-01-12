using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces.Providers;
using Serilog;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public class ChatProvider : IChatProvider
{
    private readonly ILogger _logger;

    public ChatProvider(ILogger logger)
    {
        _logger = logger;
    }

    public bool GetChat(Update update, [NotNullWhen(true)] out Chat? chat)
    {
        chat = update switch
        {
            { Message: { Chat: var messageChat } } => messageChat,
            { EditedMessage: { Chat: var editMessageChat} } => editMessageChat,
            { CallbackQuery: { Message: { Chat: var callbackChat } } } => callbackChat,
            _ => null
        };

        if (chat is null)
        {
            _logger.Warning($"Couldn't get Chat for update {update.Type}: {update.Id}");
            return false;
        }

        return true;
    }
}