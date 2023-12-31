using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserRevokedRoleToUserDomainEvent(UserId RevokerId, UserId RevokedUserId, RoleId RevokedRole) : DomainEvent(RevokedUserId)
    {
    }
}
