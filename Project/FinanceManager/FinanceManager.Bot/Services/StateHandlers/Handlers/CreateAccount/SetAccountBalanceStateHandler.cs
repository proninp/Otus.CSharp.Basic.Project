using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class SetAccountBalanceStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _updateMessageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public SetAccountBalanceStateHandler(
        IUpdateMessageProvider updateMessageProvider, IMessageManager messageManager, ISessionStateManager sessionStateManager)
    {
        _updateMessageProvider = updateMessageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!_updateMessageProvider.GetMessage(updateContext.Update, out var message))
        {
            _sessionStateManager.Wait(updateContext.Session);
            return;
        }

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The entered value is not a number. Please try again.");

            _sessionStateManager.Wait(updateContext.Session);
            return;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.InitialBalance = amount;

        _sessionStateManager.Continue(updateContext.Session, WorkflowState.CreateAccountEnd);
    }
}