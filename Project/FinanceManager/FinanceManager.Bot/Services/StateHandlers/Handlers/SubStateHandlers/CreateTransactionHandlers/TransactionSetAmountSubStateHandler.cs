using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.SubStateHandlers.CreateTransactionHandlers;
public class TransactionSetAmountSubStateHandler : ISubStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageSenderManager _messageSender;

    public TransactionSetAmountSubStateHandler(IUpdateMessageProvider messageProvider, IMessageSenderManager messageSender)
    {
        _messageProvider = messageProvider;
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
            

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageSender.SendErrorMessage(
                botClient, message.Chat,
                "The entered value is not a number. Please try again.", cancellationToken);
            session.Wait();
            return;
        }

        if (amount < 0)
        {
            await _messageSender.SendErrorMessage(
                botClient, message.Chat,
                "The expense amount must be a non-negative number. Please try again.", cancellationToken);
            session.Wait();
            return;
        }

        var context = session.GetTransactionContext();
        context.Amount = amount;

        session.Continue(WorkflowSubState.RegisterTransaction);
    }
}
