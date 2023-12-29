using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatAppendMessageDomainEvent(Chat e, Users.User messageOwner, Messages.Message message) : DomainEvent(e)
    {
    }
}
