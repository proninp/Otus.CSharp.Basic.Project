using FinanceManager.Bot.Services.Interfaces.Providers;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public class ChatProvider : IChatProvider
{
    public Chat GetChat(Update update)
    {
        var chat = update switch
        {
            { Message: { Chat: var messageChat } } => messageChat,
            { CallbackQuery: { Message: { Chat: var callbackChat } } } => callbackChat,
            _ => null
        };
        ArgumentNullException.ThrowIfNull(chat, nameof(chat));
        return chat;
    }
}