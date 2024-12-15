using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public sealed class Currency : IdentityModel
{
    public string Title { get; init; }

    public string CurrencyCode { get; init; }

    public string CurrencySign { get; init; }

    public string? Emoji { get; init; }

    public Currency(string title, string currencyCode, string currencySign, string? emoji = null)
    {
        Title = title;
        CurrencyCode = currencyCode;
        CurrencySign = currencySign;
        Emoji = emoji;
    }
}