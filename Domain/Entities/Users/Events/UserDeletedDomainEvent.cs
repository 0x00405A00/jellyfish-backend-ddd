using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserDeletedDomainEvent(UserId UserId) : DomainEvent(UserId)
    {
    }
}
