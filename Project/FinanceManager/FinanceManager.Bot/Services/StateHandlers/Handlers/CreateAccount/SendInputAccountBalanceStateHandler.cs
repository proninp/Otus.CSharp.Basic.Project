using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public sealed class SendInputAccountBalanceStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public SendInputAccountBalanceStateHandler(
        IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.SendMessage(updateContext, "Enter a number to set the initial balance:");
        return _sessionStateManager.Next(updateContext.Session);
    }
}
