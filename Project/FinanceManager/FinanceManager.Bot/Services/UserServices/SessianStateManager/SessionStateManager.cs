using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Redis.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed partial class SessionStateManager : ISessionStateManager
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
            { WorkflowState.SelectSettingsMenu, WorkflowState.CreateSettingsMenu },
            { WorkflowState.SelectManageCategoriesMenu, WorkflowState.CreateManageCategoriesMenu },
        };

        AddCreateAccountWorkflowPrevious(stateTransitions);
        AddRegistrTransactionWorkflowPrevious(stateTransitions);
        AddCreateCategoryWorkflowPrevious(stateTransitions);
        AddRemoveCategoryWorkflowPrevious(stateTransitions);
        AddRenameCategoryWorkflowPrevious(stateTransitions);

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
            { WorkflowState.CreateSettingsMenu, (WorkflowState.SelectSettingsMenu, false) },
            { WorkflowState.CreateManageCategoriesMenu, (WorkflowState.SelectManageCategoriesMenu, false) },
        };

        AddCreateAccountWorkflowNext(stateTransitions);
        AddRegisterTransactionWorkflowNext(stateTransitions);
        AddCreateCategoryWorkflowNext(stateTransitions);
        AddRemoveCategoryWorkflowNext(stateTransitions);
        AddRenameCategoryWorkflowNext(stateTransitions);

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

    public async Task<bool> ToMainMenu(UserSession session) =>
        await Continue(session, WorkflowState.CreateMenu);

    public async Task<bool> ToSettingsMenu(UserSession session) =>
        await Continue(session, WorkflowState.CreateSettingsMenu);

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

    public async Task<bool> Complete(UserSession session) => session.State switch
    {
        WorkflowState.RegisterTransaction => await ToMainMenu(session),
        WorkflowState.RegisterNewCategory => await Continue(session, WorkflowState.ManageAccounts),
        _ => await Reset(session)
    };

    private async Task SetState(UserSession session, WorkflowState newState)
    {
        session.State = newState;
        session.LastActivity = DateTime.UtcNow;
        await _redisCacheService.SaveDataAsync(session.TelegramId.ToString(), session);
    }
}