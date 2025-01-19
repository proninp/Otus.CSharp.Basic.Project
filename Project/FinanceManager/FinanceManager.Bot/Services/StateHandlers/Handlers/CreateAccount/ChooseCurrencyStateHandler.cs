using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class ChooseCurrencyStateHandler : IStateHandler
{
    private readonly ICurrencyManager _currencyManager;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ICallbackDataValidator _callbackDataValidator;

    public ChooseCurrencyStateHandler(ICurrencyManager currencyManager,
        ICallbackDataProvider callbackDataProvider,
        IMessageManager messageManager,
        ICallbackDataValidator callbackDataValidator)
    {
        _currencyManager = currencyManager;
        _callbackDataProvider = callbackDataProvider;
        _messageManager = messageManager;
        _callbackDataValidator = callbackDataValidator;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var previousState = WorkflowState.SendCurrencies;

        var callbackQuery = await _callbackDataProvider.GetCallbackData(updateContext, true, previousState);
        if (callbackQuery is null)
            return;

        if (! await _callbackDataValidator.Validate(updateContext, callbackQuery))
        {
            updateContext.Session.Wait();
            return;
        }

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
        updateContext.Session.WorkflowContext = context;

        await _messageManager.DeleteLastMessage(updateContext);
        await _messageManager.SendMessage(updateContext, "Enter a number to set the initial balance:");

        updateContext.Session.Wait(WorkflowState.SetAccountInitialBalance);
    }

    private async Task DeleteLastMessageAndContinue(BotUpdateContext updateContext, WorkflowState workflowState)
    {
        await _messageManager.DeleteLastMessage(updateContext);
        updateContext.Session.Continue(workflowState);
    }
}