using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserAssignedRoleToUserDomainEvent(User assigner, User assignUser, Roles.Role assignedRole) : DomainEvent(assignUser)
    {
    }
}
