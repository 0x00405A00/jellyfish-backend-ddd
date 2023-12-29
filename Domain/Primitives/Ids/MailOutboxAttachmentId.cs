using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record MailOutboxAttachmentId : Identification
    {
        public MailOutboxAttachmentId(Guid Id) : base(Id)
        {
        }
    }
}
