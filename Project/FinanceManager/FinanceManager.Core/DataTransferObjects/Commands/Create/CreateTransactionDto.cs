using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Create;
public class CreateTransactionDto : IPutModel<Transaction>
{
    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public Transaction ToModel() =>
        new Transaction(UserId, AccountId, CategoryId, Date, TransactionType, Amount, Description);
}
