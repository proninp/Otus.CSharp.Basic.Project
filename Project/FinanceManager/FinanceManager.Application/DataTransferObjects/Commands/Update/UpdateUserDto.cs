using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateUserDto : IdentityDtoBase
{
    public long TelegramId { get; init; }

    public string? Name { get; init; }
}