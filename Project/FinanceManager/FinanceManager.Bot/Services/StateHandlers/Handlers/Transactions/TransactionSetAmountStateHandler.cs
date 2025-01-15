using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionSetAmountStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;

    public TransactionSetAmountStateHandler(IUpdateMessageProvider messageProvider, IMessageManager messageManager)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
        {
            updateContext.Session.Wait();
            return;
        }

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The entered value is not a number. Please try again.");
            updateContext.Session.Wait();
            return;
        }

        if (amount < 0)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The expense amount must be a non-negative number. Please try again.");
            updateContext.Session.Wait();
            return;
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Amount = amount;

        updateContext.Session.Continue(WorkflowState.RegisterTransaction);
    }
}
