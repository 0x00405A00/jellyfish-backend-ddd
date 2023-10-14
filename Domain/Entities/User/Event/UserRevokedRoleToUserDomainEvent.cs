using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserRevokedRoleToUserDomainEvent(User revoker, User revokeUser, Role.Role revokedRole) : DomainEvent(revokeUser)
    {
    }
}
