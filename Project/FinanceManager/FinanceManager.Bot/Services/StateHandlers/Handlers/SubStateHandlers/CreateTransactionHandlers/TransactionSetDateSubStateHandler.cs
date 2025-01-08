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
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IMessageSenderManager _messageSender;

    public TransactionSetDateSubStateHandler(
        IUpdateMessageProvider messageProvider,
        ITransactionDateProvider transactionDateProvider,
        IMessageSenderManager messageSender)
    {
        _messageProvider = messageProvider;
        _transactionDateProvider = transactionDateProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
        {
            session.Wait();
            return;
        }

        var dateText = message.Text;

        if (!_transactionDateProvider.TryParseDate(dateText, out var date))
        {
            var incorrectDateMessage = _transactionDateProvider.GetIncorrectDateText();
            await _messageSender.SendMessage(botClient, message.Chat, incorrectDateMessage, cancellationToken);

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
            botClient, message.Chat, $"Please enter {context.TransactionTypeDescription} {emoji} amount:", cancellationToken);

        session.Wait(WorkflowSubState.SetTransactionAmount);
    }
}