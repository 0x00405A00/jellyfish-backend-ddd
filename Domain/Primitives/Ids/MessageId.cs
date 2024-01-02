namespace Domain.Primitives.Ids
{
    public record MessageId : Identification
    {
        public MessageId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
