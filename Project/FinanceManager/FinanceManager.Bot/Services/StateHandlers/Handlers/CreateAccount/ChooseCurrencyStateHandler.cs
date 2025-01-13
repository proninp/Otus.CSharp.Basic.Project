using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class ChooseCurrencyStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IUpdateCallbackQueryProvider _updateCallbackQueryProvider;
    private readonly IMessageSenderManager _messageSender;

    public ChooseCurrencyStateHandler(ICurrencyManager currencyManager,
        IUpdateCallbackQueryProvider updateCallbackQueryProvider,
        IMessageSenderManager messageSender)
    {
        _currencyManager = currencyManager;
        _updateCallbackQueryProvider = updateCallbackQueryProvider;
        _messageSender = messageSender;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.SendCurrencies;
        if (!_updateCallbackQueryProvider.GetCallbackQuery(updateContext.Update, out var callbackQuery))
        {
            updateContext.Session.Continue(previousState);
            return;
        }

        var currencyId = callbackQuery.Data;
        if (string.IsNullOrEmpty(currencyId))
        {
            updateContext.Session.Continue(previousState);
            return;
        }

        var currency = await _currencyManager.GetById(new Guid(currencyId), updateContext.CancellationToken);
        if (currency is null)
        {
            updateContext.Session.Continue(previousState);
            return;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.Currency = currency;
        updateContext.Session.WorkflowContext = context;

        await _messageSender.SendMessage(updateContext, "Enter a number to set the initial balance:");

        updateContext.Session.Wait(WorkflowState.SetAccountInitialBalance);
    }
}