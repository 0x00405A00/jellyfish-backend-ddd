namespace Domain.Primitives.Ids
{
    public record MailOutboxAttachmentId : Identification
    {
        public MailOutboxAttachmentId(Guid Id) : base(Id)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
