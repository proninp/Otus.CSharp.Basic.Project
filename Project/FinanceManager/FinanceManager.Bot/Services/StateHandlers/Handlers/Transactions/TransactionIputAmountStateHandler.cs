using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Transactions;
public class TransactionIputAmountStateHandler : IStateHandler
{
    private readonly IMessageManager _messageManager;
    private readonly IUserSessionStateManager _sessionStateManager;

    public TransactionIputAmountStateHandler(IMessageManager messageManager, IUserSessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetTransactionContext();

        var emoji = context.TransactionType switch
        {
            TransactionType.Expense => Emoji.ExpenseAmount.GetSymbol(),
            TransactionType.Income => Emoji.IncomeAmount.GetSymbol(),
            _ => string.Empty
        };

        await _messageManager.SendMessage(updateContext, $"Please enter {context.TransactionTypeDescription} {emoji} amount:");

        _sessionStateManager.Wait(updateContext.Session, WorkflowState.SetTransactionAmount);
    }
}
