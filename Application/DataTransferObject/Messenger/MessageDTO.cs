using System.Text.Json.Serialization;

namespace Application.DataTransferObject.Messenger
{
    public class MessageDTO
    {
        [JsonPropertyName("uuid")]
        public Guid? Uuid { get; set; }

        [JsonPropertyName("chat_uuid")]
        public Guid ChatId { get; set; }

        [JsonPropertyName("owner_uuid")]
        public Guid OwnerUuid { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("binary_content_base64")]
        public string? BinaryContentBase64 { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonPropertyName("deleted_time")]
        public DateTime? DeletedTime { get; set; }

        [JsonPropertyName("created_by_user_uuid")]
        public Guid? CreatedByUserUuid { get; set; }

        [JsonPropertyName("last_modified_by_user_uuid")]
        public Guid? LastModifiedByUserUuid { get; set; }

        [JsonPropertyName("deleted_by_user_uuid")]
        public Guid? DeletedByUserUuid { get; set; }
    }
}
