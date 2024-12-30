using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers.CreateAccountHandler;
public class ChooseCurrencySubStateHandler : ISubStateHandler
{
    private readonly IUpdateCallbackQueryProvider _updateCallbackQueryProvider;

    public ChooseCurrencySubStateHandler(IUpdateCallbackQueryProvider updateCallbackQueryProvider)
    {
        _updateCallbackQueryProvider = updateCallbackQueryProvider;
    }

    public Task<UserSubState> HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
