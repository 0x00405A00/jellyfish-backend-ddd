using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserCreatedDomainEvent(User e) : DomainEvent(e)
    {

    }
}
