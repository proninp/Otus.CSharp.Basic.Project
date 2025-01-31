using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class DefaultStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public DefaultStateHandler(
        IAccountManager accountManager, IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _accountManager = accountManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var session = updateContext.Session;
        var defaultAccount = await _accountManager.GetDefault(session.Id, updateContext.CancellationToken);
        if (defaultAccount is null)
        {
            var messageText = $"Hi, {session.UserName}! {Emoji.Greeting.GetSymbol()}" +
                $"{Environment.NewLine}Let's set you up!";

            await _messageManager.SendMessage(updateContext, messageText);

            _sessionStateManager.InitAccount(session);
        }
        else
        {
            _sessionStateManager.ToMenu(session);
        }
    }
}