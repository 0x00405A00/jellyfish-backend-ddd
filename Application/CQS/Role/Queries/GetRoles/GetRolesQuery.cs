using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Queries.GetRoles
{
    public sealed record GetRolesQuery() : IQuery<List<RoleDTO>>;
}
