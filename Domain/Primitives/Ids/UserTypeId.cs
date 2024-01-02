using Domain.Const;

namespace Domain.Primitives.Ids
{
    public record UserTypeId : Identification
    {
        public UserTypeId(Guid guid) : base(guid)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }

        public static UserTypeId GetAdminType() => new UserTypeId(UserConst.UserType.Admin);

        public static UserTypeId GetUserType() => new UserTypeId(UserConst.UserType.User);

        public static UserTypeId GetRootType() => new UserTypeId(UserConst.UserType.Root);
    }
}
