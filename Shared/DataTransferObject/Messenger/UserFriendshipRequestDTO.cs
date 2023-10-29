using System.Text.Json.Serialization;

namespace Shared.DataTransferObject.Messenger
{
    public class UserFriendshipRequestDTO
    {
        [JsonPropertyName("user_uuid")]
        public Guid UserUuid { get; set; }
    }
}
