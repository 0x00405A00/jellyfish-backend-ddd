using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserAssignedRoleToUserDomainEvent(User assigner, User assignUser, Role.Role assignedRole) : DomainEvent(assignUser)
    {
    }
}
