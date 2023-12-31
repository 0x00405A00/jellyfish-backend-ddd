using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Messages.Events
{
    public record MessageUpdatedDomainEvent(UserId UserId, MessageId MessageId) : DomainEvent(MessageId)
    {
    }
}
