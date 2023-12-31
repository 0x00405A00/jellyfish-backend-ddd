using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserAssignedRoleToUserDomainEvent(UserId Assigner, UserId AssignedUserId, RoleId AssignedRoleId) : DomainEvent(AssignedUserId)
    {
    }
}
