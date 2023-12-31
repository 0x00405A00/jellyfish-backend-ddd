using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats.Events
{
    public record ChatUserRevokeAdminDomainEvent(ChatId ChatId, UserId ActorUserId, UserId TargetUserId) : DomainEvent(ChatId)
    {
    }
}
