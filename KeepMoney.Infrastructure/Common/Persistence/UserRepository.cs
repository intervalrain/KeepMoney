﻿using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security;
using KeepMoney.Domain.Users;
using KeepMoney.Infrastructure.Common.Data;

namespace KeepMoney.Infrastructure.Common.Persistence;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private static bool _dataInitialized = false;
    private readonly IPasswordHasher _passwordHasher;

    public UserRepository(AppDbContext context, IPasswordHasher passwordHasher)
        : base(context)
    {
        _passwordHasher = passwordHasher;
        InitializeDataAsync().Wait();
    }

    private async Task InitializeDataAsync()
    {
        if (_dataInitialized) return;
        var user  = await GetAsync(u => u.Email == "intervalrain@gmail.com");
        if (!user.Any())
        {
            await AddAsync(User.Create(
                firstName: "Rain",
                lastName: "Hu",
                email: "intervalrain@gmail.com",
                subscription: Subscription.Create(SubscriptionType.Basic, DateTime.UtcNow.AddDays(30)),
                role: Role.Admin,
                passwordHash: _passwordHasher.Hash("12345678")));
            await SaveChangesAsync();

            _dataInitialized = true;
        }
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var result = await GetAsync(u => u.Email == email, cancellationToken: cancellationToken);
        return result.FirstOrDefault(); 
    }
}

