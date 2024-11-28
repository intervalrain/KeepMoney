namespace KeepMoney.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class User
    {
        public const string Get = "get:user";
        public const string GetAll = "get:users";
        public const string Update = "update:user";
        public const string Delete = "delete:user";
    }
}

