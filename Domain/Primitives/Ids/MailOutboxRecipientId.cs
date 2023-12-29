using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record MailOutboxRecipientId : Identification
    {
        public MailOutboxRecipientId(Guid Id) : base(Id)
        {
        }
    }
}
