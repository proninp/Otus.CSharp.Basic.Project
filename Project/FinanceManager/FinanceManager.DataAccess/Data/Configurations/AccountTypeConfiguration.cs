using FinanceManager.Core.Models;
using FinanceManager.DataAccess.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.DataAccess.Data.Configurations;
public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.HasData(DataSeeder.GetAccountTypeSeeds());
    }
}
