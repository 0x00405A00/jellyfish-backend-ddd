using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class User
{
    public string Uuid { get; set; } = null!;

    public string UserTypeUuid { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? ActivationCode { get; set; }

    public string? ActivationToken { get; set; }

    public string? PasswordResetCode { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetExpiresIn { get; set; }

    public sbyte? PasswordResetCodeConfirmation { get; set; }

    public DateTime? ActivationDatetime { get; set; }

    /// <summary>
    /// Base64 von Pic Binary
    /// </summary>
    public byte[]? Picture { get; set; }

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual ICollection<Auth> Auths { get; set; } = new List<Auth>();

    public virtual ICollection<ChatRelationToUser> ChatRelationToUsers { get; set; } = new List<ChatRelationToUser>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<MessageAcknowledge> MessageAcknowledges { get; set; } = new List<MessageAcknowledge>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserChatInviteRequest> UserChatInviteRequestTargetUserUus { get; set; } = new List<UserChatInviteRequest>();

    public virtual ICollection<UserChatInviteRequest> UserChatInviteRequestUserUus { get; set; } = new List<UserChatInviteRequest>();

    public virtual ICollection<UserFriend> UserFriendFriendUserUus { get; set; } = new List<UserFriend>();

    public virtual ICollection<UserFriend> UserFriendUserUus { get; set; } = new List<UserFriend>();

    public virtual ICollection<UserFriendshipRequest> UserFriendshipRequestTargetUserUus { get; set; } = new List<UserFriendshipRequest>();

    public virtual ICollection<UserFriendshipRequest> UserFriendshipRequestUserUus { get; set; } = new List<UserFriendshipRequest>();

    public virtual ICollection<UserRelationToRole> UserRelationToRoles { get; set; } = new List<UserRelationToRole>();

    public virtual UserType UserTypeUu { get; set; } = null!;
}
