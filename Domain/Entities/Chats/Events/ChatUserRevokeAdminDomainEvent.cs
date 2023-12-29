using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatUserRevokeAdminDomainEvent(Chat e, Users.User revoker, Users.User target) : DomainEvent(e)
    {
    }
}
