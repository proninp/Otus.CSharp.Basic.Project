using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Create;
public sealed class CreateUserDto : IPutModel<User>
{
    public long TelegramId { get; init; }

    public string? Name { get; init; }

    public User ToModel() =>
        new User(TelegramId, Name);
}