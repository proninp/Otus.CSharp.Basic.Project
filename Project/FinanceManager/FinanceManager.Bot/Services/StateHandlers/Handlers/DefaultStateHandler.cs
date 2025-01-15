﻿using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.CommandHandlers.Handlers;
public class DefaultStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IMessageManager _messageManager;

    public DefaultStateHandler(IAccountManager accountManager, IMessageManager messageManager)
    {
        _accountManager = accountManager;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var defaultAccount = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);

        if (defaultAccount is null)
        {
            var messageText = $"Hi, {updateContext.Session.UserName}! {Emoji.Greeting.GetSymbol()}" +
                $"{Environment.NewLine}Let's set you up!";
            await _messageManager.SendMessage(updateContext, messageText);

            updateContext.Session.Continue(WorkflowState.CreateAccountStart, true);
        }
        else
        {
            updateContext.Session.Continue(WorkflowState.CreateMenu, true);
        }
    }
}