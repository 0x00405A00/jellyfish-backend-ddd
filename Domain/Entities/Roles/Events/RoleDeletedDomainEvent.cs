using Domain.Primitives;

namespace Domain.Entities.Roles.Events
{
    public record RoleDeletedDomainEvent(Role e) : DomainEvent(e)
    {
    }
}
