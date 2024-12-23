using FinanceManager.Core.Models;
using FinanceManager.DataAccess.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.DataAccess.Data.Configurations;
public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasData(DataSeeder.GetCurrencySeeds());
    }
}