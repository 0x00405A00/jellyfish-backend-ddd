using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Roles.Events
{
    public record RoleCreatedDomainEvent(RoleId RoleId) : DomainEvent(RoleId)
    {
    }
}
