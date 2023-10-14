using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatUserRevokeAdminDomainEvent(Chat e, User.User revoker, User.User target) : DomainEvent(e)
    {
    }
}
