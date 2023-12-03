using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject.Messenger
{
    public class UserFriendshipRequestDTO : IDataTransferObject
    {
        [JsonPropertyName("user_uuid")]
        public Guid UserUuid { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
