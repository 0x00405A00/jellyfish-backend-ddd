using System.Text.Json.Serialization;

namespace Application.DataTransferObject.Messenger
{
    public class UserFriendshipRequestDTO
    {
        [JsonPropertyName("user_uuid")]
        public Guid UserUuid { get; set; }    
    }
}
