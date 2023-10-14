using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatUserAddToChatDomainEvent(Chat e, User.User execUser, User.User target) : DomainEvent(e)
    {
    }
}
