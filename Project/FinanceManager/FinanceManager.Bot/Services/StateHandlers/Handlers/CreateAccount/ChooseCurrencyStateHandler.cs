using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class ChooseCurrencyStateHandler : IStateHandler
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

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var callbackQuery = await _callbackDataProvider.GetCallbackData(updateContext, true);
        if (callbackQuery is null)
        {
            _sessionStateManager.Previous(updateContext.Session);
            return;
        }

        var currencyId = callbackQuery.Data;
        if (string.IsNullOrEmpty(currencyId))
        {
            await DeleteLastMessageAndContinue(updateContext);
            return;
        }

        var currency = await _currencyManager.GetById(new Guid(currencyId), updateContext.CancellationToken);
        if (currency is null)
        {
            await DeleteLastMessageAndContinue(updateContext);
            return;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.Currency = currency;

        await _messageManager.DeleteLastMessage(updateContext);

        _sessionStateManager.Next(updateContext.Session);
    }

    private async Task DeleteLastMessageAndContinue(BotUpdateContext updateContext)
    {
        await _messageManager.DeleteLastMessage(updateContext);
        _sessionStateManager.Previous(updateContext.Session);
    }
}