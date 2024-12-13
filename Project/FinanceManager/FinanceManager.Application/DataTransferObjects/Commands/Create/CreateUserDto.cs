using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.Commands.Create;
public sealed class CreateUserDto
{
    public long TelegramId { get; init; }

    public string? Username { get; init; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public User ToModel() =>
        new User(TelegramId, Username, Firstname, Lastname);
}