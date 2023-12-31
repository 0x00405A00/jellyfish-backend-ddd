using MediatR;

namespace Domain.Primitives
{
    public record DomainEvent(Identification entity) : INotification
    {
    }
}
