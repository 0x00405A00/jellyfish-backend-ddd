using Domain.Primitives;

namespace Domain.Entities.MailoutBox
{
    public sealed class MailRecipientId : Identification
    {
        public MailRecipientId(Guid guid) : base(guid)
        {
        }
    }
}
