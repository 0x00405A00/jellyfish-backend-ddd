using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserDeletedDomainEvent(User e) : DomainEvent(e)
    {
    }
}
