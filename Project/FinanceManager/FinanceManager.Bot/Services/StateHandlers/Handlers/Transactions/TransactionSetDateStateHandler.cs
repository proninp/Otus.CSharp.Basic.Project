using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;

public sealed class TransactionSetDateStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ISessionStateManager _sessionStateManager;

    public TransactionSetDateStateHandler(
        IUpdateMessageProvider messageProvider,
        ICallbackDataProvider callbackDataProvider,
        ITransactionDateProvider transactionDateProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _messageProvider = messageProvider;
        _transactionDateProvider = transactionDateProvider;
        _messageManager = messageManager;
        _callbackDataProvider = callbackDataProvider;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var dateText = await GetUpdateText(updateContext);
        if (dateText is null)
            return false;

        if (!_transactionDateProvider.TryParseDate(dateText, out var date))
        {
            var incorrectDateMessage = _transactionDateProvider.GetIncorrectDateText();
            await _messageManager.DeleteLastMessage(updateContext);
            await _messageManager.SendErrorMessage(updateContext, incorrectDateMessage);

            return await _sessionStateManager.Previous(updateContext.Session);
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Date = date;

        await _messageManager.DeleteLastMessage(updateContext);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private async Task<string?> GetUpdateText(BotUpdateContext updateContext)
    {
        string? dateText = null;
        if (_messageProvider.GetMessage(updateContext.Update, out var message))
            return message?.Text;

        var callBackData = await _callbackDataProvider.GetCallbackData(updateContext, false);
        if (callBackData is not null)
        {
            dateText = callBackData.Data;
        }
        return dateText;
    }
}