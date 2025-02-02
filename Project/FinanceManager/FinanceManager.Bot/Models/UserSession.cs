using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Models;
public sealed class UserSession
{
    public Guid Id { get; init; }

    public long TelegramId { get; init; }

    public string? UserName { get; set; }

    public WorkflowState State { get; set; }

    public WorkflowContext? WorkflowContext { get; set; }

    public required DateTime LastActivity { get; set; }

    public UserMessage? LastMessage { get; set; }

    public required string CallbackSessionId { get; init; }

    public TimeSpan Expiration { get; set; }
}

public static class UserSessionExtensions
{
    public static UserSession ToUserSession(this UserDto userDto, string callbackSessionId, int ttlMinutes = 5)
    {
        return new UserSession
        {
            Id = userDto.Id,
            TelegramId = userDto.TelegramId,
            UserName = userDto.Username,
            State = WorkflowState.Default,
            CallbackSessionId = callbackSessionId,
            LastActivity = DateTime.UtcNow,
            Expiration = TimeSpan.FromMinutes(ttlMinutes)
        };
    }
}