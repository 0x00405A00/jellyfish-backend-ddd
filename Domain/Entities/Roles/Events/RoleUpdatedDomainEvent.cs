using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Roles.Events
{
    public record RoleUpdatedDomainEvent(RoleId RoleId) : DomainEvent(RoleId)
    {
    }
}
