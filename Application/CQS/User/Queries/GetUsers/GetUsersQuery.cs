using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUsers
{
    public sealed record GetUsersQuery() : IQuery<List<UserDTO>>;
}
