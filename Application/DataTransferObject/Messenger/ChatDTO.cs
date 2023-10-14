﻿using System.Text.Json.Serialization;

namespace Application.DataTransferObject.Messenger
{
    public class ChatDTO
    {
        [JsonPropertyName("uuid")]
        public Guid? Uuid { get; set; }

        [JsonPropertyName("chat_name")]
        public string? ChatName { get; set; }

        [JsonPropertyName("chat_description")]
        public string? ChatDescription { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("members")]
        public ICollection<MessengerUserDTO>? Members { get; set; }

        [JsonPropertyName("admins")]
        public ICollection<MessengerUserDTO>? Admins { get; set; }

        [JsonPropertyName("messages")]
        public ICollection<MessageDTO>? Messages { get; set; }

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

        public ChatDTO()
        {

        }
    }
}
