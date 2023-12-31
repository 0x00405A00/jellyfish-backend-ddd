using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserLoggedInDomainEvent(UserId e) : DomainEvent(e)
    {
    }
}
