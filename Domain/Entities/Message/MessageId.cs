using Domain.Primitives;

namespace Domain.Entities.Message
{
    public class MessageId : Identification
    {
        public MessageId()
        {
            
        }
        public MessageId(Guid guid) : base(guid)
        {
        }
    }
}
