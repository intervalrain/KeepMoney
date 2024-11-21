using ErrorOr;
using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Request;

namespace KeepMoney.Infrastructure.Common.Security;

public class AuthorizationService : IAuthorizationService
{
    private readonly IPolicyEnforcer _policyEnforcer;
    private readonly ICurrentUserProvider _currentUserProvider;

    public AuthorizationService(IPolicyEnforcer policyEnforcer, ICurrentUserProvider currentUserProvider)
    {
        _policyEnforcer = policyEnforcer;
        _currentUserProvider = currentUserProvider;
    }

    public ErrorOr<Success> AuthorizeCurrentUser<T>(
        IAuthorizableRequest<T> request,
        List<string> requiredRoles,
        List<string> requiredPermissions,
        List<string> requiredPolicies)
    {
        var currentUser = _currentUserProvider.CurrentUser;

        if (requiredPermissions.Except(currentUser.Permissions).Any())
        {
            return Error.Unauthorized(description: "User is missing required permissions for taking this action");
        }

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            return Error.Unauthorized(description: "User is missing required roles for taking this action");
        }

        foreach (var policy in requiredPolicies)
        {
            var authorizationAgainstPolicyResult = _policyEnforcer.Authorize(request, currentUser, policy);

            if (authorizationAgainstPolicyResult.IsError)
            {
                return authorizationAgainstPolicyResult.Errors;
            }
        }

        return Result.Success;
    }
}