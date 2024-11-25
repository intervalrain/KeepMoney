namespace KeepMoney.Application.Common.Security.Models;

public record CurrentUser(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    List<string> Roles,
    List<string> Permissions);