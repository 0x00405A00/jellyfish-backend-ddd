namespace Domain.Primitives.Ids
{
    public record MailOutboxId : Identification
    {
        public MailOutboxId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
