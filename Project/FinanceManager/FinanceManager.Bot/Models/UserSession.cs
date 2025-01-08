using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserSession
{
    public Guid Id { get; init; }

    public long TelegramId { get; init; }

    public string? UserName { get; set; }

    public UserState UserState { get; set; } = new();

    public object? ContextData { get; set; }

    public WorkflowState State
    {
        get => UserState.State;
        set => UserState.State = value;
    }

    public WorkflowSubState SubState
    {
        get => UserState.SubState;
        set => UserState.SubState = value;
    }

    public void ResetState() 
    {
        UserState.Reset();
        ContextData = null;
    }

    public bool IsContinue() =>
        UserState.IsContinue();

    public void Wait() =>
        UserState.Wait();

    public void Wait(WorkflowSubState subState) =>
        UserState.Wait(subState);

    public void Wait(WorkflowState state) 
    {
        UserState.Wait(state);
        ContextData = null;
    }

    public void Continue() =>
        UserState.Continue();

    public void Continue(WorkflowSubState subState) =>
        UserState.Continue(subState);

    public void Continue(WorkflowState state)
    {
        UserState.Continue(state);
        ContextData = null;
    }
}

public static class UserSessionExtensions
{
    public static void SetData<T>(this UserSession session, T context)
        where T : class
    {
        session.ContextData = context;
    }

    public static T? GetData<T>(this UserSession session, string key)
        where T : class
    {
        return session.ContextData is not null ? (session.ContextData as T) : default;
    }

    public static UserSession ToUserSession(this UserDto userDto)
    {
        return new UserSession
        {
            Id = userDto.Id,
            TelegramId = userDto.TelegramId,
            UserName = userDto.Username,
            UserState = new UserState(),
        };
    }
}