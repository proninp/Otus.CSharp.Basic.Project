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
    private readonly IDecimalNumberProvider _decimalNumberProvider;

    public TransactionSetAmountStateHandler(
        IUpdateMessageProvider messageProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IDecimalNumberProvider decimalNumberProvider)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _decimalNumberProvider = decimalNumberProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
            return false;

        var amountText = message.Text;

        await _messageManager.DeleteLastMessageAsync(updateContext);

        if (!_decimalNumberProvider.Provide(message.Text, out var value))
        {
            await _messageManager.SendErrorMessageAsync(updateContext,
                "The entered value is not a number.");
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        if (value < 0)
        {
            await _messageManager.SendErrorMessageAsync(updateContext,
                "The expense amount must be a non-negative number.");
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Amount = value;

        return await _sessionStateManager.Next(updateContext.Session);
    }
}