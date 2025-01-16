using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using System.Text;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class HistoryStateHandler : IStateHandler
{
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageManager _messageManager;
    private readonly ITransactionManager _transactionManager;
    private readonly IAccountManager _accountManager;

    public HistoryStateHandler(
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IMessageManager messageManager,
        ITransactionManager transactionManager,
        IAccountManager accountManager)
    {
        _callbackQueryProvider = callbackQueryProvider;
        _messageManager = messageManager;
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

        StringBuilder messageBuilder = new StringBuilder();

        if (incomes.Count() > 0)
        {
            var incomesBuilder = new StringBuilder($"{Emoji.Income.GetSymbol()} Incomes:{Environment.NewLine}");
            incomesBuilder.AppendLine();
            incomesBuilder.AppendLine(
                string.Join(Environment.NewLine, incomes.Select(GetTransactionFormattedString)));
            messageBuilder.AppendLine(incomesBuilder.ToString());
            messageBuilder.AppendLine();
        }

        if (expenses.Count() > 0)
        {
            var expensesBuilder = new StringBuilder($"{Emoji.Expense.GetSymbol()} Expenses:{Environment.NewLine}");
            expensesBuilder.AppendLine();
            expensesBuilder.AppendLine(
                string.Join(Environment.NewLine, expenses.Select(GetTransactionFormattedString)));
            messageBuilder.AppendLine(expensesBuilder.ToString());
        }

        if (! await _messageManager.EditLastMessage(updateContext, messageBuilder.ToString()))
            await _messageManager.SendMessage(updateContext, messageBuilder.ToString());

        updateContext.Session.Continue(WorkflowState.CreateMenu);
    }

    private string GetTransactionFormattedString(TransactionDto transaction)
    {
        var transactionLine = new StringBuilder($"{Emoji.Clock.GetSymbol()} ");
        transactionLine.Append($"{transaction.Date}: ");
        transactionLine.Append($"<b>{Math.Abs(transaction.Amount)}</b> ");
        transactionLine.Append($"{transaction.Account?.Currency?.CurrencyCode}");
        if (transaction.Category is not null)
        {
            transactionLine.Append(" - ");
            transactionLine.Append($"{transaction.Category?.Emoji} {transaction.Category?.Title};");
        }
        else
        {
            if (!string.IsNullOrEmpty(transaction.Description))
            {
                transactionLine.Append(" - ");
                transactionLine.Append(transaction.Description);
            }
        }
        return transactionLine.ToString();
    }
}
