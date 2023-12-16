using Domain.Primitives;

namespace Domain.Entities.Chats
{
    public sealed class ChatId : Identification
    {
        public ChatId(Guid guid) : base(guid)
        {
        }
    }
}
