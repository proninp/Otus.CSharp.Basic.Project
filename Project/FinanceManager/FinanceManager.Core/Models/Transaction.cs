using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;

public sealed class Transaction : BaseModel
{
    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public DateTime Date { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public User User { get; }

    public Account Account { get; set; }

    public Category? Category { get; set; }

    protected Transaction() { }

    public Transaction(Guid userId, Guid accountId, Guid? categoryId = null, DateTime? date = null, decimal amount = 0, string? description = null)
    {
        UserId = userId;
        AccountId = accountId;
        CategoryId = categoryId;
        Date = date ?? DateTime.UtcNow;
        Amount = amount;
        Description = description;
    }
}
