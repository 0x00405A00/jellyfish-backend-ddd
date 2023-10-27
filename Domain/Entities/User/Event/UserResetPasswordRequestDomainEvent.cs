using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserPasswordResetCompletedDomainEvent(User e) : DomainEvent(e)
    {
    }
}