using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Update;
public sealed class UpdateUserDto : UpdateDtoBase<User>
{
    public long TelegramId { get; init; }

    public string? Name { get; init; }

    public override User ToModel() =>
        new User(TelegramId, Name);
}