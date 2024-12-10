using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateAccountDto : IdentityDtoBase
{
    public Guid UserId { get; init; }

    public string? Title { get; set; }

    public bool IsDefault { get; set; }

    public bool IsArchived { get; set; }
}