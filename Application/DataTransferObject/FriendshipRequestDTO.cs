namespace Application.DataTransferObject
{
    public class FriendshipRequestDTO
    {
        public UserDTO RequestUser { get; private set; }
        public UserDTO TargetUser { get; private set; }
        public string? TargetUserRequestMessage { get; private set; }
    }
}