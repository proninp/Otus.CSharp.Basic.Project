﻿using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.StateHandlers.Handlers.Abstractions;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public sealed class SelectMenuStateHandler : BaseChooseMenuStateHandler
{
    public SelectMenuStateHandler(
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager,
        IMessageManager messageManager)
        : base(callbackDataProvider, sessionStateManager, messageManager)
    {
    }

    public override Dictionary<string, WorkflowState> MenuStateMapping => new Dictionary<string, WorkflowState>
    {
        { MainMenu.Expense.GetKey(), WorkflowState.AddExpense },
        { MainMenu.Income.GetKey(), WorkflowState.AddIncome },
        { MainMenu.History.GetKey(), WorkflowState.History },
        { MainMenu.Settings.GetKey(), WorkflowState.CreateSettingsMenu }
    };
}