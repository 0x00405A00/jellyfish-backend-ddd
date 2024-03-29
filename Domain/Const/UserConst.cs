﻿namespace Domain.Const
{
    public struct UserConst
    {
        public static readonly Guid RootUserId = new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b");
        public static readonly string RootUserName= "Root";

        public readonly struct UserType
        {
            public UserType()
            {

            }
            public static readonly string RootName ="Root";
            public static readonly string UserName = "User";
            public static readonly string AdminName = "Admin";

            public static readonly Guid Root = new Guid("c3f257ff-1c85-4e3c-b6b2-21ef4afba71f");
            public static readonly Guid Admin = new Guid("c3f267ff-1c85-4e3c-b6b2-21ef4afba71f");
            public static readonly Guid User = new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b");
        }
    }
}
