using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Mails;

public interface IMailOutboxRecipient
{
    string Email { get; }
    EmailTypeId EmailSendingTypeForeignKey { get; }
    MailOutbox Mail { get; set; }
    MailOutboxId MailOutboxForeignKey { get; }
    EmailSendingType SendingType { get; set; }
}

public sealed partial class MailOutboxRecipient : Entity<MailOutboxRecipientId>, IMailOutboxRecipient
{
    public string Email { get; private set; }
    public MailOutboxId MailOutboxForeignKey { get; private set; }
    public EmailTypeId EmailSendingTypeForeignKey { get; private set; }

    private MailOutboxRecipient() : base()
    {

    }
    private MailOutboxRecipient(
        MailOutboxRecipientId id,
        MailOutboxId mailOutboxId,
        EmailTypeId emailTypeId,
        string email,
        CustomDateTime createdDateTime,
        CustomDateTime? modifiedDateTime,
        CustomDateTime? deletedDateTime) : base(id)
    {
        MailOutboxForeignKey = mailOutboxId;
        EmailSendingTypeForeignKey = emailTypeId;
        Email = email;
        CreatedTime = createdDateTime;
        LastModifiedTime = modifiedDateTime;
        DeletedTime = deletedDateTime;
    }
    public static MailOutboxRecipient Create(
        MailOutboxRecipientId id,
        MailOutboxId mailOutboxId,
        EmailTypeId emailTypeId,
        string email,
        CustomDateTime createdDateTime,
        CustomDateTime? modifiedDateTime,
        CustomDateTime? deletedDateTime)
    {
        return new MailOutboxRecipient(
            id,
            mailOutboxId,
            emailTypeId,
            email,
            createdDateTime,
            modifiedDateTime,
            deletedDateTime);
    }
}
public sealed partial class MailOutboxRecipient
{
    public MailOutbox Mail { get; set; }
    public EmailSendingType SendingType { get; set; }
}
