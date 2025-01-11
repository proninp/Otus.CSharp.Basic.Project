using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;

public class TransactionSetDateSubStateHandler : ISubStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IChatProvider _chatProvider;
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IMessageSenderManager _messageSender;

    public TransactionSetDateSubStateHandler(
        IUpdateMessageProvider messageProvider,
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IChatProvider chatProvider,
        ITransactionDateProvider transactionDateProvider,
        IMessageSenderManager messageSender)
    {
        _messageProvider = messageProvider;
        _callbackQueryProvider = callbackQueryProvider;
        _chatProvider = chatProvider;
        _transactionDateProvider = transactionDateProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_chatProvider.GetChat(update, out var chat))
            return;

        if (!GetUpdateText(update, out var dateText))
        {
            session.Wait();
            return;
        }

        if (!_transactionDateProvider.TryParseDate(dateText, out var date))
        {
            var incorrectDateMessage = _transactionDateProvider.GetIncorrectDateText();
            await _messageSender.SendMessage(botClient, chat, incorrectDateMessage, cancellationToken);

            session.Wait();
            return;
        }

        var context = session.GetTransactionContext();
        context.Date = date;

        var emoji = context.TransactionType switch
        {
            TransactionType.Expense => Emoji.ExpenseAmount.GetSymbol(),
            TransactionType.Income => Emoji.IncomeAmount.GetSymbol(),
            _ => string.Empty
        };

        await _messageSender.SendMessage(
            botClient, chat, $"Please enter {context.TransactionTypeDescription} {emoji} amount:", cancellationToken);

        session.Wait(WorkflowSubState.SetTransactionAmount);
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