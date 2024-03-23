using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Queries.GetFriends
{
    public sealed record GetFriendsQuery(Guid UserId) : IQuery<List<MessengerUserDTO>>;
}
