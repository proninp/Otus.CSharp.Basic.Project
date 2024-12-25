using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserSession
{
    public Guid Id { get; init; }
    
    public long TelegramId { get; init; }
    
    public string? UserName { get; set; }

    public UserState UserState { get; set; }

    public UserSubState SubState { get; set; }

    public object? ContextData { get; set; }
}

public static class UserSessionExtensions
{
    public static void SetData<T>(this UserSession session, T context)
        where T: class
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
            UserState = UserState.Default,
            SubState = UserSubState.Default,

        };
    }
}