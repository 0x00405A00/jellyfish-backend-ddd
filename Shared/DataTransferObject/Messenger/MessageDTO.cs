using System.Text.Json.Serialization;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject.Messenger
{
    public class MessageDTO : AbstractAuditableDTO, IMessage
    {
        [JsonPropertyName("chat_uuid")]
        public Guid? ChatId { get; set; }

        [JsonPropertyName("owner_uuid")]
        public Guid OwnerUuid { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("binary_content_base64")]
        public string? BinaryContentBase64 { get; set; }
        [JsonPropertyName("binary_content_mime_type")]
        public string? BinaryContentMimeType { get; set; }

        [JsonIgnore]
        public bool HasBase64ContentSet => !String.IsNullOrEmpty(this.BinaryContentBase64);

        [JsonIgnore]
        public Chat? Chat{ get; }

        [JsonIgnore]
        public ChatId? ChatForeignKey{ get; }

        [JsonIgnore]
        public MediaContent? MediaContent{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<MessageOutbox>? MessagesInOutbox{ get; }

        [JsonIgnore]
        public User? User{ get; }

        [JsonIgnore]
        public UserId? UserForeignKey{ get; }
    }
}
