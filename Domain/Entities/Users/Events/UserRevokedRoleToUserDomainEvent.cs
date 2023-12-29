using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserRevokedRoleToUserDomainEvent(User revoker, User revokeUser, Roles.Role revokedRole) : DomainEvent(revokeUser)
    {
    }
}
