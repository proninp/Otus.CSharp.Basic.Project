using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Providers;
public class HistoryContextProvider : IHistoryContextProvider
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public HistoryContextProvider(
        IAccountManager accountManager,
        ITransactionManager transactionManager,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _accountManager = accountManager;
        _transactionManager = transactionManager;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<HistoryContext?> GetHistoryContex(BotUpdateContext updateContext)
    {
        if (updateContext.Session.WorkflowContext is not null)
            return updateContext.Session.GetHistoryContext();

        var account = await _accountManager.GetDefaultAsync(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            await _messageManager.DeleteLastMessageAsync(updateContext);
            var message = "The operation cannot be performed because you do not have a default account." +
                "Please create a default account first.";
            await _messageManager.SendErrorMessageAsync(updateContext, message);
            await _sessionStateManager.InitAccount(updateContext.Session);
            return null;
        }

        var transactionsCount = await _transactionManager.GetCountAsync(
            updateContext.Session.Id, account.Id, cancellationToken: updateContext.CancellationToken);
        if (transactionsCount == 0)
        {
            await _messageManager.DeleteLastMessageAsync(updateContext);
            await _messageManager.SendMessageAsync(
                updateContext,
                "At the moment, you do not have any registered transactions on the selected account.");
            await _sessionStateManager.ToMainMenu(updateContext.Session);
            return null;
        }

        updateContext.Session.SetHistoryContext(new HistoryContext(account, transactionsCount));

        return updateContext.Session.GetHistoryContext();
    }
}
