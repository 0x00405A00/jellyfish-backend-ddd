using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Messages.Events
{
    public record MessageCreatedDomainEvent(MessageId MessageId) : DomainEvent(MessageId)
    {
    }
}
