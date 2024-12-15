using FinanceManager.Core.Models;

namespace FinanceManager.DataAccess.Seeding;
public static class DataSeeder
{
    public static Currency[] GetCurrencySeeds()
    {
        return new[]
        {
            new Currency(currencyCode: "AED", title: "United Arab Emirates Dirham", currencySign: "د.إ", emoji: "🇦🇪") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "AUD", title: "Australian Dollar", currencySign: "$", emoji: "🇦🇺") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "BRL", title: "Brazilian Real", currencySign: "R$", emoji: "🇧🇷") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "CAD", title: "Canadian Dollar", currencySign: "$", emoji: "🇨🇦") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "CHF", title: "Swiss Franc", currencySign: "CHF", emoji: "🇨🇭") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "CNY", title: "Chinese Yuan", currencySign: "¥", emoji: "🇨🇳") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "DKK", title: "Danish Krone", currencySign: "kr", emoji: "🇩🇰") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "EUR", title: "Euro", currencySign: "€", emoji: "🇪🇺") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "GBP", title: "British Pound Sterling", currencySign: "£", emoji: "🇬🇧") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "HKD", title: "Hong Kong Dollar", currencySign: "$", emoji: "🇭🇰") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "HUF", title: "Hungarian Forint", currencySign: "Ft", emoji: "🇭🇺") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "IDR", title: "Indonesian Rupiah", currencySign: "Rp", emoji: "🇮🇩") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "INR", title: "Indian Rupee", currencySign: "₹", emoji: "🇮🇳") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "JPY", title: "Japanese Yen", currencySign: "¥", emoji: "🇯🇵") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "KRW", title: "South Korean Won", currencySign: "₩", emoji: "🇰🇷") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "MXN", title: "Mexican Peso", currencySign: "$", emoji: "🇲🇽") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "MYR", title: "Malaysian Ringgit", currencySign: "RM", emoji: "🇲🇾") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "NOK", title: "Norwegian Krone", currencySign: "kr", emoji: "🇳🇴") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "NZD", title: "New Zealand Dollar", currencySign: "$", emoji: "🇳🇿") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "PHP", title: "Philippine Peso", currencySign: "₱", emoji: "🇵🇭") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "PLN", title: "Polish Zloty", currencySign: "zł", emoji: "🇵🇱") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "RUB", title: "Russian Ruble", currencySign: "₽", emoji: "🇷🇺") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "SEK", title: "Swedish Krona", currencySign: "kr", emoji: "🇸🇪") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "SGD", title: "Singapore Dollar", currencySign: "$", emoji: "🇸🇬") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "THB", title: "Thai Baht", currencySign: "฿", emoji: "🇹🇭") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "TRY", title: "Turkish Lira", currencySign: "₺", emoji: "🇹🇷") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "USD", title: "United States Dollar", currencySign: "$", emoji: "🇺🇸") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "ZAR", title: "South African Rand", currencySign: "R", emoji: "🇿🇦") { Id = Guid.NewGuid() }
        };
    }
}
