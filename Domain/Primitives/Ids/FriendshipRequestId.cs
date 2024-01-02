namespace Domain.Primitives.Ids
{
    public record FriendshipRequestId : Identification
    {
        public FriendshipRequestId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
