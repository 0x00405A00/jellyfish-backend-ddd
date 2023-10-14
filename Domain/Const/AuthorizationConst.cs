namespace Domain.Const
{
    public struct AuthorizationConst
    {
        public struct UserType
        {
            public static Guid UserTypeUuid = Guid.Parse("7340425e-a5b5-11eb-bac0-309c2364fdb6");
            public static Guid RootTypeUuid = Guid.Parse("a5f3142d-eabb-11ed-a0f1-7085c294413b");
        }
        public struct Role
        {
            public static Guid UserRoleUuid = Guid.Parse("8937df9c-fbef-11ed-8f81-7085c294413b");
            public static Guid RootRoleUuid = Guid.Parse("d411a531-fbea-11ed-8f81-7085c294413b");
            public static Guid AdminRoleUuid = Guid.Parse("776848df-084f-11ee-b2c8-7085c294413b");
        }
        public struct Policy
        {
            public const string UserPolicy = "User";
            public const string RootPolicy = "Root";
            public const string AdminPolicy = "Admin";
        }
        public struct Claims
        {

            /// <summary>
            /// When User is registered and is activated
            /// </summary>
            public const string ClaimTypeIsActivatedUser = "is_activated_user";
            /// <summary>
            /// When user is root
            /// </summary>
            public const string ClaimTypeIsRoot = "is_root";
            /// <summary>
            /// When user is admin
            /// </summary>
            public const string ClaimTypeIsAdmin = "is_admin";
            public const string ClaimTypeUserEmail = "user_email";
            public const string ClaimTypeUserUuid = "user_id";
        }
    }
}
