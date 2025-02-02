using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public sealed class SetAccountBalanceStateHandler : IStateHandler
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

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_updateMessageProvider.GetMessage(updateContext.Update, out var message))
            return false;

        var amountText = message.Text;
        if (!decimal.TryParse(amountText, out var amount))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The entered value is not a number. Please try again.");
            return false;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.InitialBalance = amount;

        return await _sessionStateManager.Next(updateContext.Session);
    }
}