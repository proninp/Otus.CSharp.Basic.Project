using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateTransferDto : IdentityDtoBase
{
    public Guid UserId { get; init; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public DateOnly Date { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public string? Description { get; set; }
}