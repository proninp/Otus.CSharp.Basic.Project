using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class SetAccountBalanceStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _updateMessageProvider;
    private readonly IMessageSenderManager _messageSender;

    public SetAccountBalanceStateHandler(IUpdateMessageProvider updateMessageProvider, IMessageSenderManager messageSender)
    {
        _updateMessageProvider = updateMessageProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(
        UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (!_updateMessageProvider.GetMessage(update, out var message))
        {
            session.Wait();
            return;
        }

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageSender.SendErrorMessage(
                botClient, message.Chat, "The entered value is not a number. Please try again.", cancellationToken);

            session.Wait();
            return;
        }

        var context = session.GetCreateAccountContext();
        context.InitialBalance = amount;

        session.Continue(WorkflowState.CreateAccountEnd);
    }
}