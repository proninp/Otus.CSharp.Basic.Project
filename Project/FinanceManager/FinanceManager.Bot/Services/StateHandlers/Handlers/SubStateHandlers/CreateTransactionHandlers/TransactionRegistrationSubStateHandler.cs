using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
public class TransactionRegistrationSubStateHandler : ISubStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IMessageSenderManager _messageSender;
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;

    public TransactionRegistrationSubStateHandler(
        IChatProvider chatProvider,
        IMessageSenderManager messageSender,
        IAccountManager accountManager,
        ITransactionManager transactionManager)
    {
        _chatProvider = chatProvider;
        _messageSender = messageSender;
        _accountManager = accountManager;
        _transactionManager = transactionManager;
    }

    public async Task HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        session.SubState = WorkflowSubState.Complete;
        var chat = _chatProvider.GetChat(update);

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

        var message = "The transaction was successfully registered" +
            $"{Environment.NewLine}Current balance: " +
            $"{balance} {account.Currency.CurrencyCode} {account.Currency.Emoji}";

        await _messageSender.SendApproveMessage(botClient, chat, message, cancellationToken);
    }
}