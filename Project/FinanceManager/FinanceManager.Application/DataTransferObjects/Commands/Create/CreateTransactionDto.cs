using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.Commands.Create;
public sealed class CreateTransactionDto : ITransactionableCommand
{
    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public DateOnly Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public Transaction ToModel() =>
        new Transaction(
            UserId,
            AccountId,
            CategoryId,
            Date,
            TransactionType is TransactionType.Expense ? -Math.Abs(Amount) : Math.Abs(Amount),
            Description);
}