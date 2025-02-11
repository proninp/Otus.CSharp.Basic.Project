using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Redis.Services.Interfaces;

namespace FinanceManager.Bot.Services.UserServices.SessianStateManager;
public sealed class SessionStateManager : ISessionStateManager
{
    private readonly ISessionStateRegistry _sessionStateRegistry;
    private readonly IRedisCacheService _redisCacheService;

    public SessionStateManager(ISessionStateRegistry sessionStateRegistry, IRedisCacheService redisCacheService)
    {
        _sessionStateRegistry = sessionStateRegistry;
        _redisCacheService = redisCacheService;
    }

    public async Task<bool> Previous(UserSession session)
    {
        
        if (_sessionStateRegistry.PreviousStatesMap.TryGetValue(session.State, out var toState))
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
        bool result;

        if (_sessionStateRegistry.NextStatesMap.TryGetValue(session.State, out var sessionBehavior))
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

    public async Task<bool> ToManageCategoriesMenu(UserSession session) =>
        await Continue(session, WorkflowState.CreateManageCategoriesMenu);

    public async Task<bool> InitAccount(UserSession session) =>
        await Continue(session, WorkflowState.CreateAccountStart);

    public async Task<bool> FromMenu(UserSession session, WorkflowState toState) =>
        await Continue(session, toState);

    public async Task<bool> Reset(UserSession session) =>
        await Continue(session, WorkflowState.Default);

    public async Task<bool> Complete(UserSession session) => session.State switch
    {
        WorkflowState.RegisterTransaction => await ToMainMenu(session),
        WorkflowState.RegisterNewCategory => await Continue(session, WorkflowState.CreateManageCategoriesMenu),
        _ => await Reset(session)
    };

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