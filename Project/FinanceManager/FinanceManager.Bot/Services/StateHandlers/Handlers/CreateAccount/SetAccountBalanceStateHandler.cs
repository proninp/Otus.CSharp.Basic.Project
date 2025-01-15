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
    private readonly IMessageManager _messageManager;

    public SetAccountBalanceStateHandler(IUpdateMessageProvider updateMessageProvider, IMessageManager messageManager)
    {
        _updateMessageProvider = updateMessageProvider;
        _messageManager = messageManager;
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
            await _messageManager.SendErrorMessage(updateContext,
                "The entered value is not a number. Please try again.");

            updateContext.Session.Wait();
            return;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.InitialBalance = amount;

        updateContext.Session.Continue(WorkflowState.CreateAccountEnd);
    }
}