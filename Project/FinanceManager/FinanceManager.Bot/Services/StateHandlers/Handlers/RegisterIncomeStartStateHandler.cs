﻿using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers;
public sealed class RegisterIncomeStartStateHandler : RegisterTransactionStateHandler
{
    public RegisterIncomeStartStateHandler(ISessionStateManager sessionStateManager) : base(sessionStateManager)
    {
    }

    private protected override void AddExpenseContext(UserSession session)
    {
        if (session.WorkflowContext?.TransactionContext is null)
        {
            session.SetTransactionContext(TransactionContext.CreateIncomeContext());
        }
    }
}