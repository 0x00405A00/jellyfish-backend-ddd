using Domain.ValueObjects;
using Shared.DataTransferObject.Messenger;

namespace Shared.Infrastructure.Backend.Interceptor.Abstraction
{
    public interface IInternalDataInterceptorApplicationInvoker
    {
        public int Priority { get; }
        Task ReceiveMessage(params MessageDTO[] data);
        Task SendMessage(params MessageDTO[] data);
        Task CreateFriendRequest(params UserFriendshipRequestDTO[] data);
        Task ReceiveFriendRequest(params UserFriendshipRequestDTO[] data);
        Task ReceiveAcceptFriendRequest(params MessengerUserDTO[] data);
        void SetPriority(int priority);
    }
}
