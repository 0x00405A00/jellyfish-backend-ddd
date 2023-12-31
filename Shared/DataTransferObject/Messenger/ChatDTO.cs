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
        [JsonPropertyName("chat_name")]
        public string? ChatName { get; set; }

        [JsonPropertyName("chat_description")]
        public string? ChatDescription { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("picture_mime_type")]
        public string? PictureMimeType { get; set; }

        [JsonPropertyName("members")]
        public ICollection<Guid>? Members { get; set; }

        [JsonPropertyName("admins")]
        public ICollection<Guid>? Admins { get; set; }

        [JsonPropertyName("messages")]
        public ICollection<MessageDTO>? Messages { get; set; }

        IReadOnlyCollection<ChatRelationToUser> IChat.Admins{ get; }

        public IReadOnlyCollection<ChatInviteRequest>? ChatInvitesToUsers{ get; }

        public IReadOnlyCollection<ChatRelationToUser>? ChatRelationToUsers{ get; }

        public string Description{ get; }

        IReadOnlyCollection<ChatRelationToUser> IChat.Members{ get; }

        IReadOnlyCollection<Message> IChat.Messages{ get; }

        public string Name{ get; }

        public Picture Picture{ get; }

        public ChatDTO()
        {

        }
    }
}
