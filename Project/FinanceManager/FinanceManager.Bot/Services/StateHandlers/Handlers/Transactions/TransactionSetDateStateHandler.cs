using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;

public class TransactionSetDateStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IMessageManager _messageManager;

    public TransactionSetDateStateHandler(
        IUpdateMessageProvider messageProvider,
        IUpdateCallbackQueryProvider callbackQueryProvider,
        ITransactionDateProvider transactionDateProvider,
        IMessageManager messageManager)
    {
        _messageProvider = messageProvider;
        _callbackQueryProvider = callbackQueryProvider;
        _transactionDateProvider = transactionDateProvider;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!GetUpdateText(updateContext.Update, out var dateText))
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

    private bool GetUpdateText(Update update, out string? dateText)
    {
        dateText = null;
        if (_callbackQueryProvider.GetCallbackQuery(update, out var callbackQuery))
        {
            dateText = callbackQuery.Data;
        }
        else
        {
            if (_messageProvider.GetMessage(update, out var message))
                dateText = message?.Text;
        }
        return dateText is not null;
    }
}