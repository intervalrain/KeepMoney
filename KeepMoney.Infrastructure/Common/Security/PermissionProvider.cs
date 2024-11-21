using System.Reflection;

using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Roles;
using KeepMoney.Domain.Users;

namespace KeepMoney.Infrastructure.Common.Security;

public class PermissionProvider : IPermissionProvider
{
    public List<string> GetPermissions(List<string> roles, SubscriptionType subscriptionType)
    {
        if (roles.Contains(Role.Admin) || subscriptionType == SubscriptionType.Pro)
        {
            return _allPermissionTypes.SelectMany(GetPermissionValues).ToList();
        }
        return GetPermissionValues(typeof(Application.Common.Security.Permissions.Permission.Transaction));
    }

    private readonly Type[] _allPermissionTypes = new Type[]
    {
        typeof(Application.Common.Security.Permissions.Permission.Transaction)
    };

    private List<string> GetPermissionValues(Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetValue(null)!)
            .ToList()!;
}

