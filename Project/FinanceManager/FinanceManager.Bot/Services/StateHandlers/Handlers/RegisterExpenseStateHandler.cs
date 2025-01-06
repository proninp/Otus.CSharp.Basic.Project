using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class RegisterExpenseStateHandler : IStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly ITransactionDateProvider _transactionDateProvider;
    private readonly IAccountManager _accountManager;
    private readonly ITransactionManager _transactionManager;

    public RegisterExpenseStateHandler(
        IChatProvider chatProvider,
        IUpdateMessageProvider messageProvider,
        ITransactionDateProvider transactionDateProvider,
        ITransactionManager transactionManager,
        IAccountManager accountManager)
    {
        _chatProvider = chatProvider;
        _messageProvider = messageProvider;
        _transactionDateProvider = transactionDateProvider;
        _transactionManager = transactionManager;
        _accountManager = accountManager;
    }

    public async Task<UserState?> HandleStateAsync(
        UserSession userSession, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (userSession.SubState)
        {
            case UserSubState.Default:
                userSession.SubState = await HandleDefaultSubState(userSession, botClient, update, cancellationToken);
                break;

            case UserSubState.SetExpenseDate:
                userSession.SubState = await HandleSetDateSubState(userSession, botClient, update, cancellationToken);
                break;

            case UserSubState.SetExpenseAmount:
                userSession.SubState = await HandleSetAmountSubState(userSession, botClient, update, cancellationToken);
                break;
        }

        if (userSession.SubState == UserSubState.Complete)
        {
            await RegisterExpense(userSession, botClient, update, cancellationToken);
            userSession.ResetState();
        }

        return userSession.UserState;
    }

    public Task RollBackAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<UserSubState> HandleDefaultSubState(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);
        await SendMessage(botClient, chat,
            $"Please enter the date (dd mm yyyy) of the expense {Enums.Emoji.Calendar.GetSymbol()}:",
            cancellationToken);
        return UserSubState.SetExpenseDate;
    }

    private async Task<UserSubState> HandleSetDateSubState(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
            return session.SubState;

        var dateText = message.Text;

        if (!_transactionDateProvider.TryParseDate(dateText, out var date))
        {
            var incorrectDateMessage = _transactionDateProvider.GetIncorrectDateText();
            await SendMessage(botClient, message.Chat, incorrectDateMessage, cancellationToken);

            return session.SubState;
        }

        session.SetData(new TransactionContext { Date = date });

        await SendMessage(botClient, message.Chat,
            $"Please enter expense {Enums.Emoji.ExpenseAmount.GetSymbol()} amount:", cancellationToken);

        return UserSubState.SetExpenseAmount;
    }

    private async Task<UserSubState> HandleSetAmountSubState(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_messageProvider.GetMessage(update, out var message))
            return session.SubState;

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            var messageText = $"{Enums.Emoji.Error.GetSymbol()} " +
                "The entered value is not a number. Please try again.";
            await SendMessage(botClient, message.Chat, messageText, cancellationToken);
            return session.SubState;
        }

        if (amount < 0)
        {
            var messageText = $"{Enums.Emoji.Error.GetSymbol()} " +
                "The expense amount must be a non-negative number. Please try again.";
            await SendMessage(botClient, message.Chat, messageText, cancellationToken);
            return session.SubState;
        }

        var context = session.GetTransactionContext();
        context.Amount = amount;
        session.ContextData = context;
        return UserSubState.Complete;
    }

    private async Task RegisterExpense(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = _chatProvider.GetChat(update);

        var context = session.GetTransactionContext();
        if (context.Amount <= 0)
        {
            var messageText = $"{Enums.Emoji.Error.GetSymbol()} " +
                "The transaction was not registered because a zero amount was entered.";
            await SendMessage(botClient, chat, messageText, cancellationToken);
            return;
        }

        var account = await _accountManager.GetDefault(session.Id, cancellationToken);
        if (account is null)
        {
            var messageText = $"{Enums.Emoji.Error.GetSymbol()} " +
                "The operation cannot be performed because you do not have a default account.";
            await SendMessage(botClient, chat, messageText, cancellationToken);
            return;
        }

        var command = new CreateTransactionDto
        {
            UserId = session.Id,
            AccountId = account.Id,
            Amount = context.Amount,
            //CategoryId
            Date = context.Date,
            TransactionType = TransactionType.Expense
        };

        var expense = await _transactionManager.Create(command, cancellationToken);

    }

    private async Task SendMessage(
        ITelegramBotClient botClient, Chat chat, string messageText, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            chat, messageText,
            parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}
