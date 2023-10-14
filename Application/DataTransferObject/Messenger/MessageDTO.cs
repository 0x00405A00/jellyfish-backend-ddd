using System.Text.Json.Serialization;

namespace Application.DataTransferObject.Messenger
{
    public class MessageDTO
    {
        [JsonPropertyName("uuid")]
        public Guid? Uuid { get; set; }

        [JsonPropertyName("chat")]
        public ChatDTO Chat { get; set; }

        [JsonPropertyName("owner")]
        public MessengerUserDTO Owner { get; set; }

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

        [JsonPropertyName("created_by_user")]
        public MessengerUserDTO? CreatedByUser { get; set; }

        [JsonPropertyName("last_modified_by_user")]
        public MessengerUserDTO? LastModifiedByUser { get; set; }

        [JsonPropertyName("deleted_by_user")]
        public MessengerUserDTO? DeletedByUser { get; set; }
    }
}
