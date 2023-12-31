using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserPasswordResetCompletedDomainEvent(UserId UserId) : DomainEvent(UserId)
    {
    }
}