using Domain.Primitives;

namespace Domain.Entities.MailoutBox
{
    public sealed class MailId : Identification
    {
        public MailId(Guid guid) : base(guid)
        {
        }
    }
}
