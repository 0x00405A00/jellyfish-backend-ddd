using System.Text.Json.Serialization;

namespace Shared.DataTransferObject.Messenger
{
    public class UserFriendshipRequestDTO : IDataTransferObject
    {
        [JsonPropertyName("user_uuid")]
        public Guid UserUuid { get; set; }
    }
}
