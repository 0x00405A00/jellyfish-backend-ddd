using Application.Abstractions.Messaging;
using Application.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(Guid Id) : IQuery<MessengerUserDTO>;
}
