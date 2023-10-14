using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.User.Queries.GetUsers
{
    public sealed record GetUsersQuery() : IQuery<List<UserDTO>>;
}
