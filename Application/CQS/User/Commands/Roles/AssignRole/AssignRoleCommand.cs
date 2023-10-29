using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.Roles.AssignRole
{
    /// <summary>
    /// Assign Role Command Record
    /// </summary>
    /// <param name="AssignerId">The user that perform the assign role action</param>
    /// <param name="UserId">The user that is the target of assigner</param>
    /// <param name="RoleId">The role that will be assign from assigner to user</param>
    public record AssignRoleCommand(Guid AssignerId,Guid UserId,Guid RoleId) : ICommand<RoleDTO>;
}
