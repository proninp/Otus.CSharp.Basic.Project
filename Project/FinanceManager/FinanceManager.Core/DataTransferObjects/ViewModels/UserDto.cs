using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.ViewModels;
public class UserDto
{
    public Guid Id { get; init; }

    public long TelegramId { get; init; }

    public string? Name { get; init; }
}
public static class UserMappings
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            TelegramId = user.TelegramId
        };
    }
}