using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.MailoutBox;

public sealed class MailOutboxRecipient : Entity<MailRecipientId>, IAuditibleCreateEntity
{
    public MailId MailId { get; private set; }
    public EmailTypeId EmailTypeId { get; private set; }
    public Email Email { get; private set; } = null!;
    public EmailType EmailType { get; private set; } = null!;
    public MailOutbox Mail { get; private set; } = null!;
    public DateTime? CreatedTime { get; private set; }
    public User.User? CreatedByUser { get; private set; }

    private MailOutboxRecipient()
    {
        // Private constructor to enforce the use of factory methods
    }

    public static MailOutboxRecipient Create(
        MailId mailId,
        EmailTypeId emailTypeId,
        Email email,
        DateTime createTime)
    {
        var recipient = new MailOutboxRecipient
        {
            CreatedTime = createTime,
            MailId = mailId,
            EmailTypeId = emailTypeId,
            Email = email ?? throw new ArgumentNullException(nameof(email), "Email cannot be null."),
        };

        return recipient;
    }

    public void SetCreated(User.User createdBy)
    {
        this.CreatedTime = DateTime.Now;
        this.CreatedByUser = createdBy;
    }
}
