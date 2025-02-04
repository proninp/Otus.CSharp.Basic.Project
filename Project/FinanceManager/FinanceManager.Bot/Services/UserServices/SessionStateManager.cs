using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Redis.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices;
public class SessionStateManager : ISessionStateManager
{
    private readonly IRedisCacheService _redisCacheService;

    public SessionStateManager(IRedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    public async Task<bool> Previous(UserSession session)
    {
        var stateTransitions = new Dictionary<WorkflowState, WorkflowState>
        {
            { WorkflowState.SelectMenu, WorkflowState.CreateMenu },
            { WorkflowState.ChooseAccountName, WorkflowState.CreateAccountStart },
            { WorkflowState.ChooseCurrency, WorkflowState.SendCurrencies },
            { WorkflowState.SetAccountInitialBalance, WorkflowState.SendInputAccountInitialBalance },
            { WorkflowState.ChooseTransactionCategory, WorkflowState.SendTransactionCategories },
            { WorkflowState.SetTransactionDate, WorkflowState.SendInputTransactionDate },
            { WorkflowState.SetTransactionAmount, WorkflowState.SendInputTransactionAmount },
            { WorkflowState.History, WorkflowState.CreateMenu },
            { WorkflowState.Settings, WorkflowState.CreateMenu }
        };

        if (stateTransitions.TryGetValue(session.State, out var toState))
        {
            await SetState(session, toState);
        }
        else
        {
            await Reset(session);
        }
        return true;
    }

    public async Task<bool> Next(UserSession session)
    {
        var stateTransitions = new Dictionary<WorkflowState, (WorkflowState next, bool isContinue)>
        {
            { WorkflowState.CreateMenu, (WorkflowState.SelectMenu, false) },

            { WorkflowState.CreateAccountStart, (WorkflowState.ChooseAccountName, false) },
            { WorkflowState.ChooseAccountName, (WorkflowState.SendCurrencies, true) },
            { WorkflowState.SendCurrencies, (WorkflowState.ChooseCurrency, false) },
            { WorkflowState.ChooseCurrency, (WorkflowState.SendInputAccountInitialBalance, true) },
            { WorkflowState.SendInputAccountInitialBalance, (WorkflowState.SetAccountInitialBalance, false) },
            { WorkflowState.SetAccountInitialBalance, (WorkflowState.CreateAccountEnd, true) },
            { WorkflowState.CreateAccountEnd, (WorkflowState.CreateMenu, true) },
            
            { WorkflowState.AddExpense, (WorkflowState.SendTransactionCategories, true) },
            { WorkflowState.AddIncome, (WorkflowState.SendTransactionCategories, true) },
            { WorkflowState.SendTransactionCategories, (WorkflowState.ChooseTransactionCategory, false) },
            { WorkflowState.ChooseTransactionCategory, (WorkflowState.SendInputTransactionDate, true) },
            { WorkflowState.SendInputTransactionDate, (WorkflowState.SetTransactionDate, false) },
            { WorkflowState.SetTransactionDate, (WorkflowState.SendInputTransactionAmount, true) },
            { WorkflowState.SendInputTransactionAmount, (WorkflowState.SetTransactionAmount, false) },
            { WorkflowState.SetTransactionAmount, (WorkflowState.RegisterTransaction, true) },
            { WorkflowState.RegisterTransaction, (WorkflowState.CreateMenu, true) },

            { WorkflowState.Settings, (WorkflowState.CreateMenu, true) },
        };

        bool result;

        if (stateTransitions.TryGetValue(session.State, out var sessionBehavior))
        {
            await SetState(session, sessionBehavior.next);
            result = sessionBehavior.isContinue;
        }
        else
        {
            result = await Reset(session);
        }
        return result;
    }

    public async Task<bool> ToMenu(UserSession session) =>
        await Continue(session, WorkflowState.CreateMenu);

    public async Task<bool> InitAccount(UserSession session) =>
        await Continue(session, WorkflowState.CreateAccountStart);

    public async Task<bool> FromMenu(UserSession session, WorkflowState toState) =>
        await Continue(session, toState);

    public async Task<bool> Reset(UserSession session) =>
        await Continue(session, WorkflowState.Default);

    private async Task<bool> Continue(UserSession session, WorkflowState withState)
    {
        session.WorkflowContext = null;
        await SetState(session, withState);
        return true;
    }

    private async Task SetState(UserSession session, WorkflowState newState)
    {
        session.State = newState;
        session.LastActivity = DateTime.UtcNow;
        await _redisCacheService.SaveDataAsync(session.TelegramId.ToString(), session);
    }
}