using ErrorOr;
using KeepMoney.Application.Common.Security.Models;
using KeepMoney.Application.Common.Security.Request;

namespace KeepMoney.Application.Common.Security;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizableRequest<T> request,
        CurrentUser currentUser,
        string policy);
}