using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public class Transfer : BaseModel
{
    public Guid UserId { get; init; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public DateOnly Date { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public string? Description { get; set; }

    public User User { get; set; }

    public Account FromAccount { get; set; }

    public Account ToAccount { get; set; }

    protected Transfer() { }

    public Transfer(Guid userId, Guid fromAccountId, Guid toAccountId, DateOnly? date, decimal fromAmount = 0, decimal toAmount = 0, string? description = null)
    {
        UserId = userId;
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        FromAmount = fromAmount;
        ToAmount = toAmount;
        Description = description;
    }
}
