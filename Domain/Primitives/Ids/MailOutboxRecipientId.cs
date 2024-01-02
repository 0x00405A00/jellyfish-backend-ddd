namespace Domain.Primitives.Ids
{
    public record MailOutboxRecipientId : Identification
    {
        public MailOutboxRecipientId(Guid Id) : base(Id)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
