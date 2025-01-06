using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class DefaultStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;

    public DefaultStateHandler(IUpdateMessageProvider messageProvider, IAccountManager accountManager)
    {
        _messageProvider = messageProvider;
        _accountManager = accountManager;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
            return userSession.UserState;

        var defaultAccount = await _accountManager.GetDefault(userSession.Id, cancellationToken);

        if (defaultAccount is null)
        {
            var sentMessage = await botClient.SendMessage(
                message.Chat, $"Hi, {userSession.UserName}! Let's set you up!",
                parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserState.AddAccount;
        }

        return UserState.Menu;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}