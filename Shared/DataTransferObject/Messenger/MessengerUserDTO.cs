using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject.Messenger
{
    public class MessengerUserDTO : AbstractUserDTO
    {
        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateOnly? DateOfBirth { get; set; }

        [JsonPropertyName("friends")]
        public ICollection<MessengerUserDTO>? Friends { get; set; }

        [JsonPropertyName("friendship_requests")]
        public ICollection<MessengerUserDTO>? FriendshipRequests { get; set; }

        [JsonPropertyName("chats")]
        public ICollection<ChatDTO>? Chats { get; set; }

        public MessengerUserDTO()
        {

        }
    }
}
