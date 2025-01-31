using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;

public class TransactionSetDateStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;
    private readonly ISessionStateManager _sessionStateManager;

    public TransactionSetDateStateHandler(
        IUpdateMessageProvider messageProvider,
        ICallbackDataProvider callbackDataProvider,
        ITransactionDateProvider transactionDateProvider,
        IMessageManager messageManager,
        ICallbackDataValidator callbackDataValidator,
        ISessionStateManager sessionStateManager)
    {
        _messageProvider = messageProvider;
        _transactionDateProvider = transactionDateProvider;
        _messageManager = messageManager;
        _callbackDataProvider = callbackDataProvider;
        _callbackDataValidator = callbackDataValidator;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var dateText = await GetUpdateText(updateContext);
        if (dateText is null)
        {
            _sessionStateManager.Wait(updateContext.Session);
            return;
        }

        if (!_transactionDateProvider.TryParseDate(dateText, out var date))
        {
            var incorrectDateMessage = _transactionDateProvider.GetIncorrectDateText();
            await _messageManager.DeleteLastMessage(updateContext);
            await _messageManager.SendErrorMessage(updateContext, incorrectDateMessage);

            _sessionStateManager.Continue(updateContext.Session, WorkflowState.SendInputTransactionDate);
            return;
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Date = date;

        await _messageManager.DeleteLastMessage(updateContext);

        _sessionStateManager.Continue(updateContext.Session, WorkflowState.SendInputTransactionAmount);
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