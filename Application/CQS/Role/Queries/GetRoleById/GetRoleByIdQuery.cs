using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Queries.GetRoleById
{
    public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleDTO>;
}
