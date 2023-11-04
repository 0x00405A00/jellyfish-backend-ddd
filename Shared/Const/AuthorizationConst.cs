namespace Shared.Const
{
    public struct AuthorizationConst
    {
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
