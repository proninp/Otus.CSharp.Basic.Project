using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public sealed class TransactionSetAmountStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public TransactionSetAmountStateHandler(
        IUpdateMessageProvider messageProvider, IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
            return false;

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The entered value is not a number. Please try again.");
            return false;
        }

        if (amount < 0)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The expense amount must be a non-negative number. Please try again.");
            return false;
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Amount = amount;

        return _sessionStateManager.Next(updateContext.Session);
    }
}