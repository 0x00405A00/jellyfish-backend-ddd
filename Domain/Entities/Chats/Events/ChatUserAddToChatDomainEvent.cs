using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatUserAddToChatDomainEvent(Chat e, Users.User execUser, Users.User target) : DomainEvent(e)
    {
    }
}
