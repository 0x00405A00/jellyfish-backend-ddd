namespace Domain.Primitives.Ids
{
    public record RoleId : Identification
    {
        public RoleId(Guid guid) : base(guid)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
