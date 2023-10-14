using System.Text.Json.Serialization;

namespace Application.DataTransferObject
{
    public class UserTypeDTO
    {
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonPropertyName("deleted_time")]
        public DateTime? DeletedTime { get; set; }

        [JsonPropertyName("created_by_user")]
        public UserDTO? CreatedByUser { get; set; }

        [JsonPropertyName("last_modified_by_user")]
        public UserDTO? LastModifiedByUser { get; set; }

        [JsonPropertyName("deleted_by_user")]
        public UserDTO? DeletedByUser { get; set; }

        public UserTypeDTO()
        {

        }
    }
}
