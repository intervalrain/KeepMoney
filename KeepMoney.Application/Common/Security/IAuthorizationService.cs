using ErrorOr;

using KeepMoney.Application.Common.Security.Request;

namespace KeepMoney.Application.Common.Security;

public interface IAuthorizationService
{
    ErrorOr<Success> AuthorizeCurrentUser<T>(
        IAuthorizableRequest<T> request,
        List<string> requiredRoles,
        List<string> requiredPermissions,
        List<string> requiredPolicies);
}