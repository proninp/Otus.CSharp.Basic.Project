using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
public abstract class CompleteStateHandler : IStateHandler
{
    private protected readonly IMessageManager _messageManager;

    protected CompleteStateHandler(IMessageManager messageManager)
    {
        _messageManager = messageManager;
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
            updateContext.Session.Reset();
        }
    }

    private protected abstract Task HandleCompleteAsync(BotUpdateContext updateContext);

}
