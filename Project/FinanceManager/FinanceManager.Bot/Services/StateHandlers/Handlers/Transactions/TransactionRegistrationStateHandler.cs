using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionRegistrationStateHandler : CompleteSubStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;

    public TransactionRegistrationStateHandler(
        IChatProvider chatProvider,
        IMessageSenderManager messageSenderManager,
        IAccountManager accountManager,
        ITransactionManager transactionManager)
        : base(chatProvider, messageSenderManager)
    {
        _accountManager = accountManager;
        _transactionManager = transactionManager;
    }

    private protected override async Task HandleCompleteAsync(
        UserSession session, ITelegramBotClient botClient, Update update, Chat chat, CancellationToken cancellationToken)
    {
        var context = session.GetTransactionContext();
        if (context.Amount <= 0)
        {
            await _messageSender.SendErrorMessage(
                botClient, chat,
                "The transaction was not registered because a zero amount was entered.", cancellationToken);
            return;
        }

        var account = await _accountManager.GetDefault(session.Id, cancellationToken);
        if (account is null)
        {
            await _messageSender.SendErrorMessage(
                botClient, chat,
                "The operation cannot be performed because you do not have a default account.", cancellationToken);
            return;
        }

        var command = new CreateTransactionDto
        {
            UserId = session.Id,
            AccountId = account.Id,
            Amount = context.Amount,
            CategoryId = context.Category?.Id,
            Date = context.Date,
            TransactionType = context.TransactionType
        };

        var transaction = await _transactionManager.Create(command, cancellationToken);
        var balance = await _accountManager.GetBalance(account, cancellationToken);

        var message = $"The transaction was successfully registered{Environment.NewLine}" +
            $"Current balance: {balance}";
        if (account.Currency is not null)
            message += $" {account.Currency.CurrencyCode} {account.Currency.Emoji}";

        await _messageSender.SendApproveMessage(botClient, chat, message, cancellationToken);
    }
}