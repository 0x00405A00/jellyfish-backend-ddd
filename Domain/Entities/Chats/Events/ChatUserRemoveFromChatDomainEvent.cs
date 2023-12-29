using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatUserRemoveFromChatDomainEvent(Chat e, Users.User execUser, Users.User target) : DomainEvent(e)
    {
    }
}
