﻿using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionSetAmountStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;    
    private readonly IMessageManager _messageManager;
    private readonly IUserSessionStateManager _sessionStateManager;

    public TransactionSetAmountStateHandler(
        IUpdateMessageProvider messageProvider, IMessageManager messageManager, IUserSessionStateManager sessionStateManager)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
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

        if (amount < 0)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The expense amount must be a non-negative number. Please try again.");
            _sessionStateManager.Wait(updateContext.Session);
            return;
        }

        var context = updateContext.Session.GetTransactionContext();
        context.Amount = amount;

        _sessionStateManager.Continue(updateContext.Session, WorkflowState.RegisterTransaction);
    }
}