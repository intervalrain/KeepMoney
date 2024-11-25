using System.Reflection;

using KeepMoney.Application.Common.Security;
using KeepMoney.Application.Common.Security.Roles;
using KeepMoney.Domain.Users;

namespace KeepMoney.Infrastructure.Common.Security;

public class PermissionProvider : IPermissionProvider
{
    public List<string> GetPermissions(List<string> roles, SubscriptionType subscriptionType)
    {
        return roles.Contains(Role.Admin) || subscriptionType == SubscriptionType.Pro
            ? _allPermissionTypes.SelectMany(GetPermissionValues).ToList()
            : _userPermissions;
    }

    private readonly List<string> _userPermissions = new()
    {
        Application.Common.Security.Permissions.Permission.Transaction.Get,
        Application.Common.Security.Permissions.Permission.Transaction.GetAll,
        Application.Common.Security.Permissions.Permission.Transaction.Set,
        Application.Common.Security.Permissions.Permission.Transaction.Update,
        Application.Common.Security.Permissions.Permission.Transaction.Delete,

        Application.Common.Security.Permissions.Permission.User.Get,
        Application.Common.Security.Permissions.Permission.User.Update,
        Application.Common.Security.Permissions.Permission.User.Delete,
    };

    private readonly Type[] _allPermissionTypes = new Type[]
    {
        typeof(Application.Common.Security.Permissions.Permission.Transaction),
        typeof(Application.Common.Security.Permissions.Permission.User)
    };

    private List<string> GetPermissionValues(Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetValue(null)!)
            .ToList()!;
}

