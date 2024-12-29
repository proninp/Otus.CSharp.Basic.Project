using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public sealed class CurrencyDto : IdentityDtoBase
{
    public string Title { get; init; }

    public string CurrencyCode { get; init; }

    public string CurrencySign { get; init; }

    public string? Emoji { get; init; }
}
public static class CurrencyMappings
{
    public static CurrencyDto ToDto(this Currency currency)
    {
        return new CurrencyDto
        {
            Id = currency.Id,
            Title = currency.Title,
            CurrencyCode = currency.CurrencyCode,
            CurrencySign = currency.CurrencySign,
            Emoji = currency.Emoji,
        };
    }
}