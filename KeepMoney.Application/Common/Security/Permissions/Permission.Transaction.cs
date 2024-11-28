namespace KeepMoney.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Transaction
    {
        public const string Set = "set:transaction";
        public const string Get = "get:transaction";
        public const string GetAll = "get:transactions";
        public const string Update = "update:transaction";
        public const string Delete = "delete:transaction";
    }
}

