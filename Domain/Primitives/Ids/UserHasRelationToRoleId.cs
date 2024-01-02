namespace Domain.Primitives.Ids
{
    public record UserHasRelationToRoleId : Identification
    {
        public UserHasRelationToRoleId(Guid guid) : base(guid)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
