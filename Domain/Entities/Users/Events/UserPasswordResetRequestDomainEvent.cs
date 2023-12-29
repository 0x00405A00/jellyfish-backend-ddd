using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserPasswordResetRequestDomainEvent(User e) : DomainEvent(e)
    {

    }
}