using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public sealed class Account : BaseModel
{
    public Guid UserId { get; init; }

    public Guid AccountTypeId { get; init; }

    public Guid CurrencyId { get; init; }

    public string? Title { get; set; }

    public decimal Balance { get; set; }

    public bool IsDefault { get; set; }

    public bool IsArchived { get; set; }

    public AccountType AccountType { get; }

    public Currency Currency { get; }

    public Account(Guid userId, Guid accountTypeId, Guid currencyId, string? title = null, decimal balance = 0, bool isDefault = false, bool isArchived = false)
    {
        UserId = userId;
        AccountTypeId = accountTypeId;
        CurrencyId = currencyId;
        Title = title;
        Balance = balance;
        IsDefault = isDefault;
        IsArchived = isArchived;
    }
}
