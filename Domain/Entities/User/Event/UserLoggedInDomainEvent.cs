using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserLoggedInDomainEvent(User e) : DomainEvent(e)
    {
    }
}
