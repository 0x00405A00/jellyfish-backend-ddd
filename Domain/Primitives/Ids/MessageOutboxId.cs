namespace Domain.Primitives.Ids
{
    public record MessageOutboxId : Identification
    {
        public MessageOutboxId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
