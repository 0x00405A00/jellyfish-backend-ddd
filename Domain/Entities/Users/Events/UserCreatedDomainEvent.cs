using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserCreatedDomainEvent(UserId UserId) : DomainEvent(UserId)
    {

    }
}
