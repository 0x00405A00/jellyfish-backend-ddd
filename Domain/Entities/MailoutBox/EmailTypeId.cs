using Domain.Primitives;

namespace Domain.Entities.MailoutBox
{
    public sealed class EmailTypeId : Identification
    {
        public EmailTypeId(Guid guid) : base(guid)
        {
        }
    }
}
