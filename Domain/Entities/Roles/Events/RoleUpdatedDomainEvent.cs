using Domain.Primitives;

namespace Domain.Entities.Roles.Events
{
    public record RoleUpdatedDomainEvent(Role e) : DomainEvent(e)
    {
    }
}
