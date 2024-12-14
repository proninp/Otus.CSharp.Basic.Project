using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserSession
{
    public Guid Id { get; init; }
    
    public long TelegramId { get; init; }
    
    public string? UserName { get; set; }

    public UserState UserState { get; set; }
}

public static class UserSessionMappings
{
    public static UserSession ToUserSession(this UserDto userDto)
    {
        return new UserSession
        {
            Id = userDto.Id,
            TelegramId = userDto.TelegramId,
            UserName = userDto.Username,
            UserState = UserState.Start
        };
    }
}