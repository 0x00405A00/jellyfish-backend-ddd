namespace Domain.Primitives.Ids
{
    public record ChatRelationToUserId : Identification
    {
        public ChatRelationToUserId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
