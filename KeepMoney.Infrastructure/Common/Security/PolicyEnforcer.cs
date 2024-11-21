using ErrorOr;

using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Models;
using KeepMoney.Application.Common.Security.Policies;
using KeepMoney.Application.Common.Security.Request;
using KeepMoney.Application.Common.Security.Roles;
using KeepMoney.Domain.Users;

namespace KeepMoney.Infrastructure.Common.Security;

public class PolicyEnforcer : IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizableRequest<T> request,
        CurrentUser currentUser,
        string policy)
    {
        return policy switch
        {
            Policy.SelfOrAdmin => SelfOrAdminPolicy(request, currentUser),
            _ => Error.Unexpected(description: "Unknown policy name"),
        };
    }

    private static ErrorOr<Success> SelfOrAdminPolicy<T>(IAuthorizableRequest<T> request, CurrentUser currentUser) =>
        request.UserId == currentUser.Id || currentUser.Roles.Contains(Role.Admin)
            ? Result.Success
            : Error.Unauthorized(description: "Requesting user failed policy requirement");
}