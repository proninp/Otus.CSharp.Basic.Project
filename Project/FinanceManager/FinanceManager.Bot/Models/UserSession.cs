using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserSession
{
    private bool _waitForUserInput;

    public Guid Id { get; init; }

    public long TelegramId { get; init; }

    public string? UserName { get; set; }

    public WorkflowState State { get; set; }

    public object? WorkflowContext { get; set; }

    public void Reset() 
    {
        State = WorkflowState.Default;
        _waitForUserInput = false;
        WorkflowContext = null;
    }

    public bool IsContinue()
    {
        var wait = _waitForUserInput;
        _waitForUserInput = false;
        return !wait;
    }

    public void Wait() =>
        _waitForUserInput = true;

    public void Wait(WorkflowState state)
    {
        State = state;
        _waitForUserInput = true;
    }

    public void Continue(WorkflowState state, bool isClearContext = false)
    {
        if (isClearContext)
            WorkflowContext = null;
        State = state;
        _waitForUserInput = false;
    }
}

public static class UserSessionExtensions
{
    public static void SetData<T>(this UserSession session, T context)
        where T : class
    {
        session.WorkflowContext = context;
    }

    public static T? GetData<T>(this UserSession session, string key)
        where T : class
    {
        return session.WorkflowContext is not null ? (session.WorkflowContext as T) : default;
    }

    public static UserSession ToUserSession(this UserDto userDto)
    {
        return new UserSession
        {
            Id = userDto.Id,
            TelegramId = userDto.TelegramId,
            UserName = userDto.Username,
            State = WorkflowState.Default
        };
    }
}