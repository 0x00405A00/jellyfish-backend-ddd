using Domain.Entities.MailoutBox.Exception;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.MailoutBox;

public sealed class MailOutbox : Entity<MailId>
{
    public Email From { get; private set; } = null!;
    public string? Subject { get; private set; }
    public byte[]? Body { get; private set; }
    public bool BodyIsHtml { get; private set; }
    private List<MailOutboxAttachment> _mailOutboxAttachments = new List<MailOutboxAttachment>();
    public IReadOnlyList<MailOutboxAttachment> MailOutboxAttachments { get => _mailOutboxAttachments?.AsReadOnly(); }
    private List<MailOutboxRecipient> _mailOutboxRecipients = new List<MailOutboxRecipient>();
    public IReadOnlyList<MailOutboxRecipient> MailOutboxRecipients { get => _mailOutboxRecipients?.AsReadOnly(); }
    public DateTime? CreatedTime { get; private set; } = null!;
    public User.User? CreatedByUser { get; private set; } = null!;

    private MailOutbox()
    {
    }

    public static MailOutbox Create(
        MailId id,
        Email from,
        string subject,
        byte[] body,
        bool bodyIsHtml,
        List<MailOutboxRecipient> mailOutboxRecipients,
        List<MailOutboxAttachment> mailOutboxAttachments,
        User.User createdBy)
    {
        var mailOutbox = new MailOutbox
        {
            Id = id,
            From = from,
            Subject = subject,
            Body = body,
            BodyIsHtml = bodyIsHtml,
            CreatedTime = DateTime.Now,
            CreatedByUser = createdBy
        };
        mailOutbox._mailOutboxAttachments = mailOutboxAttachments;
        mailOutbox._mailOutboxRecipients = mailOutboxRecipients;
        return mailOutbox;
    }

    public void AddAttachment(MailOutboxAttachment attachment)
    {
        if (attachment == null)
        {
            throw new ArgumentNullException(nameof(attachment), "Attachment cannot be null.");
        }
        if (!_mailOutboxAttachments.Any(x => x.AttachmentSha1 == attachment.AttachmentSha1))
        {
            throw new AttachmentAlreadyExistsException("Recipient cannot be null.");
        }

        _mailOutboxAttachments.Add(attachment);
    }

    public void AddRecipient(MailOutboxRecipient recipient)
    {
        if (recipient == null)
        {
            throw new ArgumentNullException(nameof(recipient), "Recipient cannot be null.");
        }
        if(!_mailOutboxRecipients.Any(x=>x.Email == recipient.Email))
        {
            throw new RecipientAlreadyAddedException(nameof(recipient), "Recipient cannot be null.");
        }
        _mailOutboxRecipients.Add(recipient);
    }
}

