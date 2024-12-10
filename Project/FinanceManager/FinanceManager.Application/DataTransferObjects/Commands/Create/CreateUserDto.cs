using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.Commands.Create;
public sealed class CreateUserDto
{
    public long TelegramId { get; init; }

    public string? Name { get; init; }

    public User ToModel() =>
        new User(TelegramId, Name);
}