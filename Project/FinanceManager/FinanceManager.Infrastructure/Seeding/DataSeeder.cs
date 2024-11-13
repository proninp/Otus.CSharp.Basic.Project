using FinanceManager.Core.Models;

namespace FinanceManager.Infrastructure.Seeding;
public static class DataSeeder
{
    public static Currency[] GetCurrencySeeds()
    {
        return new[]
        {
            new Currency(currencyCode: "RUB", title: "Russian Ruble", currencySign: "₽") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "BYN", title: "Belarusian Ruble", currencySign: "Br") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "USD", title: "United States Dollar", currencySign: "$") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "EUR", title: "Euro", currencySign: "€") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "GBP", title: "British Pound Sterling", currencySign: "£") { Id = Guid.NewGuid() },
            new Currency(currencyCode: "TRY", title: "Turkish Lira", currencySign: "₺") { Id = Guid.NewGuid() },
        };
    }

    public static AccountType[] GetAccountTypeSeeds()
    {
        return new[]
        {
            new AccountType("Cash") { Id = Guid.NewGuid() },
            new AccountType("Debit/credit card") { Id = Guid.NewGuid() },
            new AccountType("Checking") { Id = Guid.NewGuid() },
            new AccountType("Loan") { Id = Guid.NewGuid() },
            new AccountType("Deposit") { Id = Guid.NewGuid() },
        };
    }
}
