using Domain.Entities.Mails.Exceptions;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Mails;

public sealed partial class MailOutbox : Entity<MailOutboxId>
{
    private List<MailOutboxAttachment> _attachments = new List<MailOutboxAttachment>();
    private List<MailOutboxRecipient> _recipients = new List<MailOutboxRecipient>();

    public string From { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public bool? IsBodyHtml { get; private set; }

    private MailOutbox() : base()
    {

    }

    private MailOutbox(
        MailOutboxId id,
        string from,
        string subject,
        string body,
        bool isBodyHtml,
        CustomDateTime createdDateTime,
        CustomDateTime? modifiedDateTime,
        CustomDateTime? deletedDateTime) : base(id)
    {
        From = from;
        Subject = subject;
        Body = body;
        IsBodyHtml = isBodyHtml;
        CreatedTime = createdDateTime;
        LastModifiedTime = modifiedDateTime;
        DeletedTime = deletedDateTime;
    }
    public static MailOutbox Create(
        MailOutboxId id,
        string from,
        string subject,
        string body,
        bool isBodyHtml,
        CustomDateTime createdDateTime,
        CustomDateTime? modifiedDateTime,
        CustomDateTime? deletedDateTime)
    {
        return new MailOutbox(
            id,
            from,
            subject,
            body,
            isBodyHtml,
            createdDateTime,
            modifiedDateTime,
            deletedDateTime);
    }

    public void AddAttachment(MailOutboxAttachment attachment)
    {
        if (attachment == null)
        {
            throw new ArgumentNullException(nameof(attachment), "Attachment cannot be null.");
        }
        if (!_attachments.Any(x => x.AttachmentSha1 == attachment.AttachmentSha1))
        {
            throw new AttachmentAlreadyExistsException("Recipient cannot be null.");
        }

        _attachments.Add(attachment);
    }

    public void AddRecipient(MailOutboxRecipient recipient)
    {
        if (recipient == null)
        {
            throw new ArgumentNullException(nameof(recipient), "Recipient cannot be null.");
        }
        if (!_recipients.Any(x => x.Email == recipient.Email))
        {
            throw new RecipientAlreadyAddedException(nameof(recipient), "Recipient cannot be null.");
        }
        _recipients.Add(recipient);
    }
}
public sealed partial class MailOutbox
{

    public ICollection<MailOutboxAttachment> Attachments { get => _attachments; }
    public ICollection<MailOutboxRecipient> Recipients { get => _recipients; }
}

