using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers;
internal class UpdateCallbackQueryProvider : IUpdateCallbackQueryProvider
{
    public Task<CallbackQuery> GetCallbackQuery(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}