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
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;

    public DefaultStateHandler(
        IChatProvider chatProvider, IAccountManager accountManager, IMessageSenderManager messageSender)
    {
        _chatProvider = chatProvider;
        _accountManager = accountManager;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_chatProvider.GetChat(update, out var chat))
            return;

        var defaultAccount = await _accountManager.GetDefault(session.Id, cancellationToken);

        if (defaultAccount is null)
        {
            var messageText = $"Hi, {session.UserName}! {Emoji.Greeting.GetSymbol()}" +
                $"{Environment.NewLine}Let's set you up!";
            await _messageSender.SendMessage(botClient, chat, messageText, cancellationToken);

            session.Continue(WorkflowState.CreateAccountStart);
        }
        else
        {
            session.Continue(WorkflowState.CreateMenu);
        }
    }
}