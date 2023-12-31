using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserActivatedDomainEvent(UserId UserId) : DomainEvent(UserId)
    {

    }
}
