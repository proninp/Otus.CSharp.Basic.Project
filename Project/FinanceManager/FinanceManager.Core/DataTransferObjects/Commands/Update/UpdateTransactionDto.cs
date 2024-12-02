using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Update;
public sealed class UpdateTransactionDto : UpdateDtoBase<Transaction>
{
    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public DateOnly Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public override Transaction ToModel() =>
        new Transaction(UserId,
            AccountId,
            CategoryId,
            Date,
            TransactionType is TransactionType.Expense ? -Math.Abs(Amount) : Math.Abs(Amount),
            Description);

}
