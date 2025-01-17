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
    private readonly IMessageManager _messageManager;
    private readonly IHistoryMessageTextProvider _historyMessageTextProvider;
    private readonly ITransactionManager _transactionManager;
    private readonly IAccountManager _accountManager;

    public HistoryStateHandler(
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IMessageManager messageManager,
        IHistoryMessageTextProvider historyMessageTextProvider,
        ITransactionManager transactionManager,
        IAccountManager accountManager)
    {
        _callbackQueryProvider = callbackQueryProvider;
        _messageManager = messageManager;
        _historyMessageTextProvider = historyMessageTextProvider;
        _transactionManager = transactionManager;
        _accountManager = accountManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            await _messageManager.DeleteLastMessage(updateContext);
            var message = "The operation cannot be performed because you do not have a default account." +
                "Please create a default account first.";
            await _messageManager.SendErrorMessage(updateContext, message);
            updateContext.Session.Continue(WorkflowState.CreateAccountStart, true);
            return;
        }

        var transactionsCount = await _transactionManager.GetCount(updateContext.Session.Id, account.Id, updateContext.CancellationToken);
        if (transactionsCount == 0)
        {
            await _messageManager.DeleteLastMessage(updateContext);
            await _messageManager.SendMessage(
                updateContext,
                "At the moment, you do not have any registered transactions on the selected account.");
            updateContext.Session.Continue(WorkflowState.CreateMenu, true);
            return;
        }

        var transactions = await _transactionManager.Get(updateContext.Session.Id, updateContext.CancellationToken);
        var incomes = transactions.Where(t => t.Amount > 0);
        var expenses = transactions.Where(t => t.Amount < 0);

        var messageText = _historyMessageTextProvider.GetMessgaText(incomes, expenses);

        if (!await _messageManager.EditLastMessage(updateContext, messageText))
            await _messageManager.SendMessage(updateContext, messageText);

        updateContext.Session.Continue(WorkflowState.CreateMenu);
    }
}