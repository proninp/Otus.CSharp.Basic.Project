using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.ViewModels;
public class TransactionDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid AccoutId { get; init; }

    public Guid? CategoryId { get; init; }

    public DateTime Date { get; init; }

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