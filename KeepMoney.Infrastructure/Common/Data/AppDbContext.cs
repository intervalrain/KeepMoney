using KeepMoney.Domain.Transactions;
using KeepMoney.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace KeepMoney.Infrastructure.Common.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}

