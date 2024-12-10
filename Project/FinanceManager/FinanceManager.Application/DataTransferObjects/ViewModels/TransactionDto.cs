using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public sealed class TransactionDto : IdentityDtoBase
{
    public Guid UserId { get; init; }

    public Guid AccoutId { get; init; }

    public Guid? CategoryId { get; init; }

    public DateOnly Date { get; init; }

    public decimal Amount { get; init; }

    public string? Description { get; init; }
}

public static class TransactionMappings
{
    public static TransactionDto ToDto(this Transaction entry)
    {
        return new TransactionDto
        {
            Id = entry.Id,
            UserId = entry.UserId,
            AccoutId = entry.AccountId,
            CategoryId = entry.CategoryId,
            Date = entry.Date,
            Amount = entry.Amount,
            Description = entry.Description,
        };
    }
}