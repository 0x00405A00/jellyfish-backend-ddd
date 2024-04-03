using System.Text.Json.Serialization;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject.Messenger
{
    public class ChatDTO : AbstractAuditableDTO, IChat
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("picture_mime_type")]
        public string? PictureMimeType { get; set; }

        [JsonPropertyName("members")]
        public ICollection<Guid>? Members { get; set; } = new List<Guid>();

        [JsonPropertyName("admins")]
        public ICollection<Guid>? Admins { get; set; } = new List<Guid>();

        [JsonPropertyName("members_as_string")]
        public ICollection<string>? MembersAsUsernames { get; set; } = new List<string>();

        [JsonPropertyName("admins_as_string")]
        public ICollection<string>? AdminsAsUsernames { get; set; } = new List<string>();

        [JsonPropertyName("messages")]
        public ICollection<MessageDTO>? Messages { get; set; }

        [JsonIgnore]
        IReadOnlyCollection<ChatRelationToUser>? IChat.Admins{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<ChatInviteRequest>? ChatInvitesToUsers{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<ChatRelationToUser>? ChatRelationToUsers{ get; }

        [JsonIgnore]
        IReadOnlyCollection<ChatRelationToUser>? IChat.Members{ get; }

        [JsonIgnore]
        IReadOnlyCollection<Message>? IChat.Messages{ get; }

        [JsonIgnore]
        public Picture? Picture{ get; }

        public ChatDTO()
        {

        }
    }
}
