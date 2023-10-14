using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserLoggedOutDomainEvent(User e) : DomainEvent(e)
    {
    }
}
