namespace Domain.Primitives.Ids
{
    public record UserHasRelationToFriendId : Identification
    {
        public UserHasRelationToFriendId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
