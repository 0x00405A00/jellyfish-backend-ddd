namespace Domain.Const
{
    public readonly struct EmailConst
    {
        public EmailConst()
        {

        }
        public readonly struct Type
        {
            public Type()
            {

            }
            public const string ToName = "To";
            public const string CcName = "Cc";
            public const string BccName = "Bcc";

            public static readonly Guid To = new Guid("c4f357ff-1c85-4e3c-b6b2-21ef4afba71f");
            public static readonly Guid Cc = new Guid("c62db414-765b-46dd-bf40-ef7d5a5abd7b");
            public static readonly Guid Bcc = new Guid("c52db414-765b-46dd-bf40-ef7d5a5abd7b");
        }
    }
}
