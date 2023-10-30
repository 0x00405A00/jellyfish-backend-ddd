using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Commands.CreateRole
{
    public record CreateRoleCommand(Guid CreatedBy,
                                    string Name,
                                    string Description) : ICommand<RoleDTO>;
}
