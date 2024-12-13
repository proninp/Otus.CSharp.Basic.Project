using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public sealed class UserDto : IdentityDtoBase
{
    public long TelegramId { get; init; }

    public string? Username { get; init; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }
}

public static class UserMappings
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            TelegramId = user.TelegramId,
            Username = user.Username,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
        };
    }
}