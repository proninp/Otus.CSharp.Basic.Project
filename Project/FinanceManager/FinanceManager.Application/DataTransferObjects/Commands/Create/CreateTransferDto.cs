using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.Commands.Create;
public sealed class CreateTransferDto
{
    public Guid UserId { get; init; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public DateOnly Date { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public string? Description { get; set; }

    public Transfer ToModel() =>
        new Transfer(UserId, FromAccountId, ToAccountId, Date, FromAmount, ToAmount, Description);
}