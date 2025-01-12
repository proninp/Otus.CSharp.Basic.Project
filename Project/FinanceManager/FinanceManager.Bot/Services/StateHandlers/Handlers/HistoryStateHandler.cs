using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class HistoryStateHandler : IStateHandler
{
    private readonly IChatProvider _chatProvider;
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageSenderManager _messageSender;
    private readonly ITransactionManager _transactionManager;
    private readonly IAccountManager _accountManager;

    public HistoryStateHandler(
        IChatProvider chatProvider,
        IUpdateCallbackQueryProvider callbackQueryProvider,
        IMessageSenderManager messageSender,
        ITransactionManager transactionManager,
        IAccountManager accountManager)
    {
        _chatProvider = chatProvider;
        _callbackQueryProvider = callbackQueryProvider;
        _messageSender = messageSender;
        _transactionManager = transactionManager;
        _accountManager = accountManager;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_chatProvider.GetChat(update, out var chat))
            return;

        var account = await _accountManager.GetDefault(session.Id, cancellationToken);
        if (account is null)
        {
            var message = "The operation cannot be performed because you do not have a default account." +
                "Please create a default account first.";
            await _messageSender.SendErrorMessage(botClient, chat, message, cancellationToken);
            session.Continue(WorkflowState.Default);
            return;
        }

        var transactionsCount = await _transactionManager.GetCount(session.Id, account.Id, cancellationToken);
        if (transactionsCount == 0)
        {
            await _messageSender.SendMessage(
                botClient, chat,
                "At the moment, you do not have any registered transactions on the selected account.",
                cancellationToken);
            session.Continue(WorkflowState.Default);
            return;
        }


        throw new NotImplementedException();
    }
}
