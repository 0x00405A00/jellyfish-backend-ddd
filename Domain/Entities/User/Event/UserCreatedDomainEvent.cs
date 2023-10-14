using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserCreatedDomainEvent(User e) : DomainEvent(e)
    {
    }
}
