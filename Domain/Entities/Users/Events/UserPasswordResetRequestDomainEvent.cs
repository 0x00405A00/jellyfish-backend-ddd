using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserPasswordResetRequestDomainEvent(UserId UserId) : DomainEvent(UserId)
    {

    }
}