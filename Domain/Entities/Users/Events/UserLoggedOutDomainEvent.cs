using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserLoggedOutDomainEvent(User e) : DomainEvent(e)
    {
    }
}
