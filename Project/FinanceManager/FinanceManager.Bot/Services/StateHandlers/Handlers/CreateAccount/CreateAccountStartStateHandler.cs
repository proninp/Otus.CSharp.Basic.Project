﻿using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class CreateAccountStartStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly IUserSessionStateManager _sessionStateManager;

    public CreateAccountStartStateHandler(IMessageManager messageManager, IUserSessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.SendMessage(updateContext, "Please enter the account name:");
        _sessionStateManager.Wait(updateContext.Session, WorkflowState.ChooseAccountName);
    }
}