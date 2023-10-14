using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.User.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserDTO>;
}
