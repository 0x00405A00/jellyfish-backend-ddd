using Domain.Primitives;

namespace Domain.Entities.Role.Event
{
    public record RoleDeletedDomainEvent(Role e) : DomainEvent(e)
    {
    }
}
