using MediatR;

namespace Domain.Primitives
{
    public record DomainEvent(Entity entity) : INotification
    {
    }
}
