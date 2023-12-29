using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatUserAssignAdminChatDomainEvent(Chat e, Users.User assigner, Users.User target) : DomainEvent(e)
    {
    }
}
