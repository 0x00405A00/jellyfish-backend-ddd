using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserLoggedInDomainEvent(User e) : DomainEvent(e)
    {
    }
}
