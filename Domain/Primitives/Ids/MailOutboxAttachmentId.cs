using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record MailOutboxAttachmentId : Identification
    {
        public MailOutboxAttachmentId(Guid Id) : base(Id)
        {
        }
    }
}
