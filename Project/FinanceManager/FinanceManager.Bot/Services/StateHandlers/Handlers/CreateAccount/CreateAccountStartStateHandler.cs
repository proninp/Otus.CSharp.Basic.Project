using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public class CreateAccountStartStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;

    public CreateAccountStartStateHandler(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        await _messageManager.SendMessage(updateContext, "Please enter the account name:");
        updateContext.Session.Wait(WorkflowState.ChooseAccountName);
    }
}