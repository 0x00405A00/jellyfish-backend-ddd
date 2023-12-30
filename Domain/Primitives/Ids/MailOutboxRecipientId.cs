using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record MailOutboxRecipientId : Identification
    {
        public MailOutboxRecipientId(Guid Id) : base(Id)
        {
        }
    }
}
