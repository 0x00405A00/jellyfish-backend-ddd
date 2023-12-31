using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Messages.Events
{
    public record MessageAcknowlegedByUserDomainEvent(MessageId MessageId, UserId UserId) : DomainEvent(MessageId)
    {
    }
}
