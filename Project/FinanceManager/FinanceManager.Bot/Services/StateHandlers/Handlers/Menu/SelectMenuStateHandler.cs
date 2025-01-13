using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class SelectMenuStateHandler : IStateHandler
{
    private readonly IUpdateCallbackQueryProvider _callbackQueryProvider;

    public SelectMenuStateHandler(IUpdateCallbackQueryProvider callbackQueryProvider)
    {
        _callbackQueryProvider = callbackQueryProvider;
    }

    public Task HandleAsync(UserSession session, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (_callbackQueryProvider.GetCallbackQuery(update, out var callbackQuery) && callbackQuery.Data is not null)
        {
            var stateMapping = new Dictionary<string, WorkflowState>
            {
                { Enums.Menu.Expense.GetKey(), WorkflowState.AddExpense },
                { Enums.Menu.Income.GetKey(), WorkflowState.AddIncome },
                { Enums.Menu.History.GetKey(), WorkflowState.History },
                { Enums.Menu.Settings.GetKey(), WorkflowState.Settings }
            };

            if (!stateMapping.TryGetValue(callbackQuery.Data, out var newState))
                newState = session.State;

            session.Continue(newState);
        }
        else
        {
            session.Reset();
        }

        return Task.CompletedTask;
    }
}
