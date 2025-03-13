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
    private readonly IDecimalNumberProvider _decimalNumberProvider;

    public SetAccountBalanceStateHandler(
        IUpdateMessageProvider updateMessageProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        IDecimalNumberProvider decimalNumberProvider)
    {
        _updateMessageProvider = updateMessageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _decimalNumberProvider = decimalNumberProvider;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_updateMessageProvider.GetMessage(updateContext.Update, out var message))
            return false;


        if (!_decimalNumberProvider.Provide(message.Text, out var value))
        {
            await _messageManager.SendErrorMessageAsync(updateContext,
                "The entered value is not a number. Please try again.");
            return false;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.InitialBalance = value;

        return await _sessionStateManager.Next(updateContext.Session);
    }
}