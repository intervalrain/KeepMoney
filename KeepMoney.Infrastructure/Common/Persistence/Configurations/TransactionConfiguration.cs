using KeepMoney.Domain.Transactions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepMoney.Infrastructure.Common.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Date)
               .IsRequired();

        builder.Property(t => t.Category)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(t => t.Amount)
               .IsRequired()
               .HasPrecision(18, 2);

        builder.Property(t => t.Note)
               .HasMaxLength(500);

        builder.HasOne(t => t.User)
               .WithMany()
               .HasForeignKey(t => t.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}