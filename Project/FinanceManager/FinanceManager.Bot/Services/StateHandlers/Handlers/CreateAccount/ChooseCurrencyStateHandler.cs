using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public sealed class ChooseCurrencyStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ISessionStateManager _sessionStateManager;

    public ChooseCurrencyStateHandler(ICurrencyManager currencyManager,
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager)
    {
        _currencyManager = currencyManager;
        _callbackDataProvider = callbackDataProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var callbackQuery = await _callbackDataProvider.GetCallbackDataAsync(updateContext, true);
        if (callbackQuery is null)
            return await _sessionStateManager.Previous(updateContext.Session);

        var currencyId = callbackQuery.Data;
        if (string.IsNullOrEmpty(currencyId))
            return await DeleteLastMessageAndContinue(updateContext);

        var currency = await _currencyManager.GetByIdAsync(new Guid(currencyId), updateContext.CancellationToken);
        if (currency is null)
            return await DeleteLastMessageAndContinue(updateContext);

        var context = updateContext.Session.GetCreateAccountContext();
        context.Currency = currency;

        await _messageManager.DeleteLastMessageAsync(updateContext);

        return await _sessionStateManager.Next(updateContext.Session);
    }

    private async Task<bool> DeleteLastMessageAndContinue(BotUpdateContext updateContext)
    {
        await _messageManager.DeleteLastMessageAsync(updateContext);
        return await _sessionStateManager.Previous(updateContext.Session);
    }
}