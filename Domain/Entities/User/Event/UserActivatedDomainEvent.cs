using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserActivatedDomainEvent(User e) : DomainEvent(e)
    {

    }
}
