using Infrastructure.Abstractions;

namespace Infrastructure.DatabaseEntity;

public partial class MailOutbox : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public string From { get; set; } = null!;

    public string? Subject { get; set; }

    public byte[]? Body { get; set; }
    public sbyte? BodyIsHtml { get; set; }

    public virtual ICollection<MailOutboxAttachment> MailOutboxAttachments { get; set; } = new List<MailOutboxAttachment>();

    public virtual ICollection<MailOutboxRecipient> MailOutboxRecipients { get; set; } = new List<MailOutboxRecipient>();
}
