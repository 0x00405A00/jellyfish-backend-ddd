using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatUserAssignAdminChatDomainEvent(Chat e, User.User assigner, User.User target) : DomainEvent(e)
    {
    }
}
