using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Common.Security.Models;

public record CurrentUser(
    Guid Id,
    Guid SubscriptionId,
    string FirstName,
    string LastName,
    string Email,
    Role Role);