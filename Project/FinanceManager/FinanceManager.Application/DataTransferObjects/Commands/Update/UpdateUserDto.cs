using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateUserDto : IdentityDtoBase
{
    public long TelegramId { get; init; }

    public string? Username { get; init; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }
}