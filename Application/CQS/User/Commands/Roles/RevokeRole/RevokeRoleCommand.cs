using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.User.Commands.Roles.RevokeRole
{
    /// <summary>
    /// Revoke a role for user
    /// </summary>
    /// <param name="RevokerId">That user that performe the revoke with target UserId</param>
    /// <param name="UserId">The user that get revoked the role</param>
    /// <param name="RoleId">The Role that should revoked</param>
    public record RevokeRoleCommand(Guid RevokerId,Guid UserId, Guid RoleId) : ICommand<RoleDTO>;
}
