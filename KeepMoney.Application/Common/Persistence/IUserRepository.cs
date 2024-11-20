using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Common.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}