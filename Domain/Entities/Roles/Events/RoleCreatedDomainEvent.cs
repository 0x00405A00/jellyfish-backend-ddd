using Domain.Primitives;

namespace Domain.Entities.Roles.Events
{
    public record RoleCreatedDomainEvent(Role e) : DomainEvent(e)
    {
    }
}
