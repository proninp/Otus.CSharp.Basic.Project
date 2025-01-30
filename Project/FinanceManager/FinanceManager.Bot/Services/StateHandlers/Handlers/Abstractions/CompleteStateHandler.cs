using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class CompleteStateHandler : IStateHandler
{
    private protected readonly IMessageManager _messageManager;
    private readonly IUserSessionStateManager _sessionStateManager;

    protected CompleteStateHandler(IMessageManager messageManager, IUserSessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        try
        {
            await HandleCompleteAsync(updateContext);
        }
        catch (Exception)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "An error occurred while performing the action. Please try again later.");
            throw;
        }
        finally
        {
            _sessionStateManager.ResetSession(updateContext.Session);
        }
    }

    private protected abstract Task HandleCompleteAsync(BotUpdateContext updateContext);
}
