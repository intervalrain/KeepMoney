using KeepMoney.Domain.Transactions;
using KeepMoney.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepMoney.Infrastructure.Common.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(256);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.Roles)
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.OwnsOne(u => u.Subscription, subscription =>
        {
            subscription.Property(s => s.SubscriptionType)
                        .IsRequired()
                        .HasConversion<string>();

            subscription.Property(s => s.ExpiryDate)
                        .IsRequired();
        });

        builder.HasMany<Transaction>()
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property<List<Guid>>("_transactionIds")
               .HasField("_transactionIds")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(id => Guid.Parse(id))
                          .ToList());
    }
}

