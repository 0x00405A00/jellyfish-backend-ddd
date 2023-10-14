using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatRemoveMessageDomainEvent(Chat e, User.User execUser, User.User messageOwner,Message.Message message) : DomainEvent(e)
    {
    }
}
