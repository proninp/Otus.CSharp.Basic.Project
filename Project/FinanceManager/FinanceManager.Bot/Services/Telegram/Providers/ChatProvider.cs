using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces.Providers;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.Telegram.Providers;
public class ChatProvider : IChatProvider
{
    public bool GetChat(Update update, [NotNullWhen(true)] out Chat? chat)
    {
        chat = update switch
        {
            { Message: { Chat: var messageChat } } => messageChat,
            { EditedMessage: { Chat: var editMessageChat} } => editMessageChat,
            { CallbackQuery: { Message: { Chat: var callbackChat } } } => callbackChat,
            _ => null
        };
        return chat is not null;
    }
}