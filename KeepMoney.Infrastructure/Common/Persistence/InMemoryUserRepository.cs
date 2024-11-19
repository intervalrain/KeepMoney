using KeepMoney.Application.Common.Persistence;
using KeepMoney.Application.Common.Security;
using KeepMoney.Domain.Users;

namespace KeepMoney.Infrastructure.Common.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly List<User> _users;

    public InMemoryUserRepository(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _users = new()
        {
            User.Create(
            firstName: "Rain",
            lastName: "Hu",
            email: "intervalrain@gmail.com",
            subscriptionType: SubscriptionType.Basic,
            role: Role.Admin,
            passwordHash: passwordHasher.Hash("12345678"))
        };
    }

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Email == email));
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }
}

