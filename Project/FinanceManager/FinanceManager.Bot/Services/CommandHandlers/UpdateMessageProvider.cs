using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers;
public class UpdateMessageProvider : IUpdateMessageProvider
{
    public Task<Message> GetMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
