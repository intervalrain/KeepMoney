using ErrorOr;
using KeepMoney.Application.Common.Security.Request;
using KeepMoney.Application.Common.Security.Roles;
using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Users.Queries.GetUsersQuery;

[Authorize(Permissions = Common.Security.Permissions.Permission.User.GetAll, Roles = Role.Admin)]
public record GetUsersQuery(Guid UserId) : IAuthorizableRequest<ErrorOr<List<User>>>;