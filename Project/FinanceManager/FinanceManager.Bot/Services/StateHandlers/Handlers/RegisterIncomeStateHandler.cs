using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class RegisterIncomeStateHandler : IStateHandler
{

    private readonly IUpdateCallbackQueryProvider _updateCallbackQueryProvider;

    public RegisterIncomeStateHandler(IUpdateCallbackQueryProvider updateCallbackQueryProvider)
    {
        _updateCallbackQueryProvider = updateCallbackQueryProvider;
    }

    public Task<UserState?> HandleStateAsync(UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
