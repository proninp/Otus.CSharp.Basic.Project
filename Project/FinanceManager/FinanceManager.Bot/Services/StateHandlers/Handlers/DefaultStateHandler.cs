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
    private readonly IUserSessionStateManager _sessionStateManager;

    public DefaultStateHandler(
        IAccountManager accountManager, IMessageManager messageManager, IUserSessionStateManager sessionStateManager)
    {
        _accountManager = accountManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var nextState = WorkflowState.CreateMenu;

        var defaultAccount = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (defaultAccount is null)
        {
            var messageText = $"Hi, {updateContext.Session.UserName}! {Emoji.Greeting.GetSymbol()}" +
                $"{Environment.NewLine}Let's set you up!";
            await _messageManager.SendMessage(updateContext, messageText);

            nextState = WorkflowState.CreateAccountStart;
        }

        _sessionStateManager.Continue(updateContext.Session, nextState, true);
    }
}