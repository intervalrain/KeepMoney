using KeepMoney.Domain.Users;

namespace KeepMoney.Application.Common.Security;

public interface IPermissionProvider
{
    List<string> GetPermissions(List<string> roles, SubscriptionType subscriptionType);
}

