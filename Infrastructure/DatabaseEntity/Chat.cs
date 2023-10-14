using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class Chat : DatabaseEntityModel
{

    public Guid Uuid { get; set; } = Guid.Empty;

    public string? Name { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// Base64 von Pic Binary
    /// </summary>
    public byte[]? Picture { get; set; }

    public Guid OwnerUserUuid { get; set; } = Guid.Empty;

    public virtual ICollection<ChatRelationToUser> ChatRelationToUsers { get; set; } = new List<ChatRelationToUser>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? OwnerUserUu { get; set; }

    public virtual ICollection<UserChatInviteRequest> UserChatInviteRequests { get; set; } = new List<UserChatInviteRequest>();
}
