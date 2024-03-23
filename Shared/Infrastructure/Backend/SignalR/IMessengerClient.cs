using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Shared.Infrastructure.Backend.SignalR
{
    /// <summary>
    /// Defines Methods that should invoked to client
    /// Direction is 'from server/hub to client'
    /// The advantage of this strongly typed client method defines is: you cant missspell them 
    /// The client muss implement these functions to communicate with the backend without issues
    /// All methods should trigger polling action by the client to request new data e.g. the client receive the message ReceiveMessage with given Messages he will now request the api endpoint to receive the new messages
    /// </summary>
    public interface IMessengerClient
    {
        /// <summary>
        /// Informs the client that he can receive new messages
        /// </summary>
        /// <param name="MessageIds">A bunch of new messages</param>
        /// <returns></returns>
        public Task ReceiveMessage(List<Guid> MessageIds);
        /// <summary>
        /// Informs the client that he received a new friendship request
        /// </summary>
        /// <param name="request">Friendship invitation</param>
        /// <returns></returns>
        public Task ReceiveFriendshipRequest(UserFriendshipRequestDTO request);
        /// <summary>
        /// Informs the client that the user <paramref name="userDTO"/> accepted the friendshiprequest
        /// </summary>
        /// <param name="userDTO">User that accept friendshiprequest</param>
        /// <returns></returns>
        public Task AcceptFriendshipRequest(UserDTO userDTO);
    }
}
