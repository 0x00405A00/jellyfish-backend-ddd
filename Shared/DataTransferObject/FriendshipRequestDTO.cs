using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class FriendshipRequestDTO : IDataTransferObject
    {
        public UserDTO RequestUser { get; private set; }
        public UserDTO TargetUser { get; private set; }
        public string? TargetUserRequestMessage { get; private set; }
    }
}