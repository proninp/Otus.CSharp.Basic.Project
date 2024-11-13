using FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands;
public class PutUserDto : BasePutDto<User>
{
    public Guid? Id { get; init; }

    public long TelegramId { get; init; }

    public string? Name { get; init; }

    public override User ToModel() =>
        new User(TelegramId, Name);
}
