using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class ChooseCurrencyStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;
    private readonly ISessionStateManager _sessionStateManager;

    public ChooseCurrencyStateHandler(ICurrencyManager currencyManager,
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ICallbackDataValidator callbackDataValidator,
        ISessionStateManager sessionStateManager)
    {
        _currencyManager = currencyManager;
        _callbackDataProvider = callbackDataProvider;
        _messageManager = messageManager;
        _callbackDataValidator = callbackDataValidator;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.SendCurrencies;

        var callbackQuery = await _callbackDataProvider.GetCallbackData(updateContext, true, previousState);
        if (callbackQuery is null)
            return;

        var currencyId = callbackQuery.Data;
        if (string.IsNullOrEmpty(currencyId))
        {
            await DeleteLastMessageAndContinue(updateContext, previousState);
            return;
        }

        var currency = await _currencyManager.GetById(new Guid(currencyId), updateContext.CancellationToken);
        if (currency is null)
        {
            await DeleteLastMessageAndContinue(updateContext, previousState);
            return;
        }

        var context = updateContext.Session.GetCreateAccountContext();
        context.Currency = currency;

        await _messageManager.DeleteLastMessage(updateContext);
        
        _sessionStateManager.Continue(updateContext.Session, WorkflowState.SendInputAccountInitialBalance);
    }

    private async Task DeleteLastMessageAndContinue(BotUpdateContext updateContext, WorkflowState workflowState)
    {
        await _messageManager.DeleteLastMessage(updateContext);
        _sessionStateManager.Continue(updateContext.Session, workflowState);
    }
}