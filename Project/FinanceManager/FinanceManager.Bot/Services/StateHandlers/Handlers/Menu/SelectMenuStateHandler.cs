using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class SelectMenuStateHandler : IStateHandler
{
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;
    private readonly IMessageManager _messageManager;

    public SelectMenuStateHandler(IUpdateCallbackQueryProvider callbackQueryProvider, IMessageManager messageManager)
    {
        _callbackQueryProvider = callbackQueryProvider;
        _messageManager = messageManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        if (_callbackQueryProvider.GetCallbackQuery(updateContext.Update, out var callbackQuery) && callbackQuery.Data is not null)
        {
            var stateMapping = new Dictionary<string, WorkflowState>
            {
                { Enums.Menu.Expense.GetKey(), WorkflowState.AddExpense },
                { Enums.Menu.Income.GetKey(), WorkflowState.AddIncome },
                { Enums.Menu.History.GetKey(), WorkflowState.History },
                { Enums.Menu.Settings.GetKey(), WorkflowState.Settings }
            };

            if (!stateMapping.TryGetValue(callbackQuery.Data, out var newState))
                newState = updateContext.Session.State;

            updateContext.Session.Continue(newState, true);
        }
        else
        {
            await _messageManager.DeleteLastMessage(updateContext);
            updateContext.Session.Continue(WorkflowState.CreateMenu);
        }

        return;
    }
}
