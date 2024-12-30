using System.Diagnostics.CodeAnalysis;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers;
public class UpdateMessageProvider : IUpdateMessageProvider
{
    public bool GetMessage(Update update, [NotNullWhen(true)] out Message? message)
    {
        message = update.Message;
        return message is not null;
    }
}