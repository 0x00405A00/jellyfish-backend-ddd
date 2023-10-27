using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserPasswordResetRequestDomainEvent(User e) : DomainEvent(e)
    {

    }
}