using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Messenger.User.Queries.GetFriendshipRequests
{
    public sealed record GetFriendshipRequestsQuery(Guid UserId) : IQuery<List<FriendshipRequestDTO>>;
}
