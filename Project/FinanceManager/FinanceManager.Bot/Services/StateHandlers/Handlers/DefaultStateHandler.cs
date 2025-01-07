using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class DefaultStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageSenderManager _messageSender;

    public DefaultStateHandler(
        IUpdateMessageProvider messageProvider, IAccountManager accountManager, IMessageSenderManager messageSender)
    {
        _messageProvider = messageProvider;
        _accountManager = accountManager;
        _messageSender = messageSender;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
            return userSession.UserState;

        var defaultAccount = await _accountManager.GetDefault(userSession.Id, cancellationToken);

        if (defaultAccount is null)
        {
            var messageText = $"Hi, {userSession.UserName}! {Enums.Emoji.Greeting.GetSymbol()}" +
                $"{Environment.NewLine}Let's set you up!";
            await _messageSender.SendMessage(botClient, message.Chat, messageText, cancellationToken);

            return UserState.AddAccount;
        }

        return UserState.Menu;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}