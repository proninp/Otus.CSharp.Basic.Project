using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public sealed class CreateAccountStartStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public CreateAccountStartStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.SendMessageAsync(updateContext, "Please enter the account name:");
        return await _sessionStateManager.Next(updateContext.Session);
    }
}