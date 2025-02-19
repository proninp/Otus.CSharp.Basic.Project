using FinanceManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations;
public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasOne<User>()
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.UserId)
            .IsRequired();

        builder.HasOne(a => a.Currency)
            .WithMany()
            .HasForeignKey(a => a.CurrencyId)
            .IsRequired();
    }
}
