using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class Chat
{
    public string Uuid { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// Base64 von Pic Binary
    /// </summary>
    public byte[]? Picture { get; set; }

    public string? OwnerUserUuid { get; set; }

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual ICollection<ChatRelationToUser> ChatRelationToUsers { get; set; } = new List<ChatRelationToUser>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? OwnerUserUu { get; set; }

    public virtual ICollection<UserChatInviteRequest> UserChatInviteRequests { get; set; } = new List<UserChatInviteRequest>();
}
