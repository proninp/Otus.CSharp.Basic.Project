﻿using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserSession
{
    private bool _waitForUserInput;

    /// <summary>
    /// Database user ID
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Telegram ID of the user's account
    /// </summary>
    public long TelegramId { get; init; }

    /// <summary>
    /// The username specified in the telegram account
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// The state in which the user is at the moment of interaction with the telegram bot
    /// </summary>
    public WorkflowState State { get; set; }

    /// <summary>
    /// An object that contains temporary information about the status of actions with a running state
    /// </summary>
    public object? WorkflowContext { get; set; }

    /// <summary>
    /// Date and time of session creation
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// The last message sent by the telegram bot
    /// </summary>
    public UserMessage? LastMessage { get; set; }

    /// <summary>
    /// Identifies the current session of the user. This ID is embedded in callback query messages and used to ensure that 
    /// user interactions are consistent within the same session. If the <see cref="CallbackSessionId"/> in the callback query differs from 
    /// the user's active session, the interaction is handled accordingly.
    /// </summary>
    public required string CallbackSessionId { get; init; }

    /// <summary>
    /// Stores the TimeSpan of the user's session lifetime
    /// </summary>
    public TimeSpan Expiration { get; set; }

    /// <summary>
    /// Resets session to Default state and clears WorkflowContext
    /// Sets the process to Continue mode
    /// </summary>
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

    public static UserSession ToUserSession(this UserDto userDto, string callbackSessionId)
    {
        return new UserSession
        {
            Id = userDto.Id,
            TelegramId = userDto.TelegramId,
            UserName = userDto.Username,
            State = WorkflowState.Default,
            CallbackSessionId = callbackSessionId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}