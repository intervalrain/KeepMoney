using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Common.Security.Models;

public record CurrentUser(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    Role Role);