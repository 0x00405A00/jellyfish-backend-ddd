using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserLoggedOutDomainEvent(UserId UserId) : DomainEvent(UserId)
    {
    }
}
