using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatAppendMessageDomainEvent(Chat e, User.User messageOwner, Message.Message message) : DomainEvent(e)
    {
    }
}
