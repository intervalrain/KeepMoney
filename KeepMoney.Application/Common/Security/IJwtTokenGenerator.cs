using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Common.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string email, string firstName, string lastName, List<string> roles, List<string> permissions);
}

