namespace Domain.Primitives.Ids
{
    public record ChatInviteRequestId : Identification
    {
        public ChatInviteRequestId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
