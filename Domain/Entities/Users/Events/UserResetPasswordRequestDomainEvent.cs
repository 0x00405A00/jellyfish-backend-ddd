using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserPasswordResetCompletedDomainEvent(User e) : DomainEvent(e)
    {
    }
}