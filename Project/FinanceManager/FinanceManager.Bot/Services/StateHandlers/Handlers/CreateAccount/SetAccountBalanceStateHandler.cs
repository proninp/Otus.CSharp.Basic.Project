using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

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

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!_updateMessageProvider.GetMessage(updateContext.Update, out var message))
        {
            updateContext.Session.Wait();
            return;
        }

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageSender.SendErrorMessage(updateContext,
                "The entered value is not a number. Please try again.");

            updateContext.Session.Wait();
            return;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.InitialBalance = amount;

        updateContext.Session.Continue(WorkflowState.CreateAccountEnd);
    }
}