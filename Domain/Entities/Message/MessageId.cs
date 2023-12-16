using Domain.Primitives;

namespace Domain.Entities.Message
{
    public sealed class MessageId : Identification
    {
        public MessageId(Guid guid) : base(guid)
        {
        }
    }
}
