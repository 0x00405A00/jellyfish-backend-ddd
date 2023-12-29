using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserActivatedDomainEvent(User e) : DomainEvent(e)
    {

    }
}
