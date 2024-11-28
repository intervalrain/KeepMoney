using ErrorOr;
using KeepMoney.Application.Common.Security.Policies;
using KeepMoney.Application.Common.Security.Request;
using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Users.Queries.GetUserQuery;

[Authorize(Permissions = Common.Security.Permissions.Permission.User.Get, Policies = Policy.SelfOrAdmin)]
public record GetUserQuery(Guid UserId) : IAuthorizableRequest<ErrorOr<User?>>;