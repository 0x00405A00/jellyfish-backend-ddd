using Domain.Primitives;

namespace Domain.Entities.Role.Event
{
    public record RoleUpdatedDomainEvent(Role e) : DomainEvent(e)
    {
    }
}
