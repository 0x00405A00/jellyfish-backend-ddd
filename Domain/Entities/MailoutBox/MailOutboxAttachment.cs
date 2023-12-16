using Domain.Primitives;

namespace Domain.Entities.MailoutBox;

public sealed class MailOutboxAttachment : Entity<MailAttachmentId>, IAuditibleCreateEntity
{
    public MailId MailId { get; private set; }
    public string MimeMediatype { get; private set; } = null!;
    public string MimeMediasubtype { get; private set; } = null!;
    public string Filename { get; private set; } = null!;
    public string MimeCid { get; private set; } = null!;
    public int Order { get; private set; }
    public string AttachmentPath { get; private set; }
    public string AttachmentSha1 { get; private set; } = null!;
    public bool IsEmbeddedInHtml { get; private set; } = false;
    public MailOutbox Mails { get; private set; } = null!;
    public DateTime? CreatedTime { get; private set; }
    public User.User? CreatedByUser { get; private set; }

    private MailOutboxAttachment()
    {
        // Private constructor to enforce the use of the factory method
    }

    public static MailOutboxAttachment Create(
        MailAttachmentId id,
        MailId mailId,
        string mimeMediatype,
        string mimeMediasubtype,
        string filename,
        string mimeCid,
        int order,
        string attachmentPath,
        string attachmentSha1,
        bool isEmbeddedInHtml,
        DateTime createTime)
    {
        var attachment = new MailOutboxAttachment
        {
            CreatedTime = createTime,
            Id = id,
            MailId = mailId,
            MimeMediatype = mimeMediatype,
            MimeMediasubtype = mimeMediasubtype,
            Filename = filename,
            MimeCid = mimeCid,
            Order = order,
            AttachmentPath = attachmentPath,
            AttachmentSha1 = attachmentSha1,
            IsEmbeddedInHtml = isEmbeddedInHtml
        };

        return attachment;
    }
    public void SetCreated(User.User createdBy)
    {
        this.CreatedTime = DateTime.Now;
        this.CreatedByUser = createdBy;
    }
}
