using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class HistoryStateHandler : IStateHandler
{
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageSenderManager _messageSender;
    private readonly ITransactionManager _transactionManager;
    private readonly IAccountManager _accountManager;

    public HistoryStateHandler(
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IMessageSenderManager messageSender,
        ITransactionManager transactionManager,
        IAccountManager accountManager)
    {
        _callbackQueryProvider = callbackQueryProvider;
        _messageSender = messageSender;
        _transactionManager = transactionManager;
        _accountManager = accountManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            var message = "The operation cannot be performed because you do not have a default account." +
                "Please create a default account first.";
            await _messageSender.SendErrorMessage(updateContext, message);
            updateContext.Session.Continue(WorkflowState.Default);
            return;
        }

        var transactionsCount = await _transactionManager.GetCount(updateContext.Session.Id, account.Id, updateContext.CancellationToken);
        if (transactionsCount == 0)
        {
            await _messageSender.SendMessage(
                updateContext,
                "At the moment, you do not have any registered transactions on the selected account.");
            updateContext.Session.Continue(WorkflowState.Default);
            return;
        }

        throw new NotImplementedException();
    }
}
