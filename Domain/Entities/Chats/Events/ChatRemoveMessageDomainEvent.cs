using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatRemoveMessageDomainEvent(Chat e, Users.User execUser, Users.User messageOwner, Messages.Message message) : DomainEvent(e)
    {
    }
}
