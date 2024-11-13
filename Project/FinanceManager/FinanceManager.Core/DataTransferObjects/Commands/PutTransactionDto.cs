using FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands;
public class PutTransactionDto : BasePutDto<Transaction>
{
    public Guid? Id { get; init; }

    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public override Transaction ToModel() =>
        new Transaction(UserId, AccountId, CategoryId, Date, Amount, Description);
}
