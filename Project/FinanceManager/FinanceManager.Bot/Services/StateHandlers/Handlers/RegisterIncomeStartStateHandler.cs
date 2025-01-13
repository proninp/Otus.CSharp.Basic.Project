﻿using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.CommandHandlers.Contexts;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class RegisterIncomeStartStateHandler : RegisterTransactionStateHandler
{
    private protected override void AddExpenseContext(UserSession session)
    {
        if (session.WorkflowContext is null)
        {
            session.SetData(new TransactionContext { TransactionType = TransactionType.Income });
        }
    }
}