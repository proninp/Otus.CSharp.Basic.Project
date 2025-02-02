using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class CompleteStateHandler : IStateHandler
{
    private protected readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    protected CompleteStateHandler(IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
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
            await _sessionStateManager.Reset(updateContext.Session);
        }
        return true;
    }

    private protected abstract Task HandleCompleteAsync(BotUpdateContext updateContext);
}
