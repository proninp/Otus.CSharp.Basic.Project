using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class StartStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;

    public StartStateHandler(IUpdateMessageProvider messageProvider, IAccountManager accountManager)
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

        // If there is no default account, switch to the account creation context
        if (defaultAccount is null)
        {
            var sentMessage = await botClient.SendMessage(
                message.Chat, $"Hi, {userSession.UserName}! Let's set you up.",
                parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            return UserState.AddAccount;
        }

        // If the default account has been created, return to the normal menu

        throw new NotImplementedException();
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}