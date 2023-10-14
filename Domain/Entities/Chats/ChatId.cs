using Domain.Primitives;

namespace Domain.Entities.Chats
{
    public class ChatId : Identification
    {
        public ChatId()
        {
            
        }
        public ChatId(Guid guid) : base(guid)
        {
        }
    }
}
