using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Roles.Events
{
    public record RoleDeletedDomainEvent(RoleId RoleId) : DomainEvent(RoleId)
    {
    }
}
