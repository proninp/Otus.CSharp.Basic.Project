using FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands;
public class PutTransferDto : BasePutDto<Transfer>
{
    public Guid? Id { get; init; }

    public Guid UserId { get; init; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public DateTime Date { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public string? Description { get; set; }

    public override Transfer ToModel() =>
        new Transfer(UserId, FromAccountId, ToAccountId, Date, FromAmount, ToAmount, Description);
}
