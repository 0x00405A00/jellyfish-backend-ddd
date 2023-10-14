using Application.Abstractions.Messaging;
using Application.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Queries.GetUsers
{
    public sealed record GetUsersQuery() : IQuery<List<MessengerUserDTO>>;
}
