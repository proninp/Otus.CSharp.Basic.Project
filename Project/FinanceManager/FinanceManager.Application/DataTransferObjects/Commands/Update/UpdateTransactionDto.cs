using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateTransactionDto : IdentityDtoBase, ITransactionableCommand
{
    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public DateOnly Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }
}