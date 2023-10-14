using Domain.Primitives;

namespace Domain.Entities.Role.Event
{
    public record RoleCreatedDomainEvent(Role e) : DomainEvent(e)
    {
    }
}
