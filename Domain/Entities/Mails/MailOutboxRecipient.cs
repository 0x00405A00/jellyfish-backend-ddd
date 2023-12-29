using Domain.Primitives;
using Domain.ValueObjects;
using Shared.ValueObjects.Ids;

namespace Domain.Entities.Mails;

public sealed class MailOutboxRecipient : Entity<MailOutboxRecipientId>
{
    public MailId MailId { get; private set; }
    public EmailTypeId EmailTypeId { get; private set; }
    public Email Email { get; private set; } = null!;
    public EmailType EmailType { get; private set; } = null!;
    public MailOutbox Mail { get; private set; } = null!;
    public DateTime? CreatedTime { get; private set; }
    public Users.User? CreatedByUser { get; private set; }

    private MailOutboxRecipient()
    {
        // Private constructor to enforce the use of factory methods
    }

    public static MailOutboxRecipient Create(
        MailOutboxId mailId,
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

    public void SetCreated(Users.User createdBy)
    {
        CreatedTime = DateTime.Now;
        CreatedByUser = createdBy;
    }
}
