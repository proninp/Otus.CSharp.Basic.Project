using ExtendedNumerics;
using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public sealed class AccountDto : IdentityDtoBase
{
    public Guid UserId { get; init; }

    public Guid CurrencyId { get; init; }

    public CurrencyDto? Currency { get; set; }

    public string? Title { get; set; }

    public BigDecimal Balance { get; set; }

    public bool IsDefault { get; set; }

    public bool IsArchived { get; set; }
}

public static class AccountMappings
{
    public static AccountDto ToDto(this Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            Title = account.Title,
            CurrencyId = account.CurrencyId,
            Currency = account.Currency?.ToDto(),
            IsDefault = account.IsDefault,
            IsArchived = account.IsArchived,
        };
    }
}