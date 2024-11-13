using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects;
public class CurrencyDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string CurrencyCode { get; init; }

    public string CurrencySign { get; init; }
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
            CurrencySign = currency.CurrencySign
        };
    }
}