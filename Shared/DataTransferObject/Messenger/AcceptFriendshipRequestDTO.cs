using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject.Messenger
{
    public class AcceptFriendshipRequestDTO : IDataTransferObject
    {
        [JsonPropertyName("request_user_uuid")]
        public Guid RequestUserUuid { get; set; }
        [JsonPropertyName("target_user_uuid")]
        public Guid TargetUserUuid { get; set; }
    }
}
