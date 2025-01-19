using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;

public class TransactionSetDateStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;

    public TransactionSetDateStateHandler(
        IUpdateMessageProvider messageProvider,
        ICallbackDataProvider callbackDataProvider,
        ITransactionDateProvider transactionDateProvider,
        IMessageManager messageManager,
        ICallbackDataValidator callbackDataValidator)
    {
        _messageProvider = messageProvider;
        _transactionDateProvider = transactionDateProvider;
        _messageManager = messageManager;
        _callbackDataProvider = callbackDataProvider;
        _callbackDataValidator = callbackDataValidator;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var dateText = await GetUpdateText(updateContext);
        if (dateText is null)
        {
            updateContext.Session.Wait();
            return;
        }

        if (!_transactionDateProvider.TryParseDate(dateText, out var date))
        {
            var incorrectDateMessage = _transactionDateProvider.GetIncorrectDateText();
            await _messageManager.DeleteLastMessage(updateContext);
            await _messageManager.SendErrorMessage(updateContext, incorrectDateMessage);

            updateContext.Session.Continue(WorkflowState.SendTransactionDateSelection);
            return;
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Date = date;

        var emoji = context.TransactionType switch
        {
            TransactionType.Expense => Emoji.ExpenseAmount.GetSymbol(),
            TransactionType.Income => Emoji.IncomeAmount.GetSymbol(),
            _ => string.Empty
        };

        await _messageManager.DeleteLastMessage(updateContext);
        await _messageManager.SendMessage(updateContext, $"Please enter {context.TransactionTypeDescription} {emoji} amount:");

        updateContext.Session.Wait(WorkflowState.SetTransactionAmount);
    }

    private async Task<string?> GetUpdateText(BotUpdateContext updateContext)
    {
        string? dateText = null;
        if (_messageProvider.GetMessage(updateContext.Update, out var message))
            return message?.Text;
        
        var callBackData = await _callbackDataProvider.GetCallbackData(updateContext, false);
        if (callBackData is not null)
        {
            if (await _callbackDataValidator.Validate(updateContext, callBackData))
                dateText = callBackData.Data;
        }
        return dateText;
    }
}