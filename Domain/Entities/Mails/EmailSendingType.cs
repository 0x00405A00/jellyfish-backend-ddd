﻿using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Mails;

public interface IEmailSendingType
{
    IReadOnlyCollection<MailOutboxRecipient> MailOutboxRecipients { get; }
    string Name { get; }
}

public sealed partial class EmailSendingType : Entity<EmailTypeId>, IEmailSendingType
{
    public string Name { get; private set; }

    private EmailSendingType() : base()
    {

    }
    private EmailSendingType(
            EmailTypeId id,
            string name,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime) : base(id)
    {
        Name = name;
        CreatedTime = createdDateTime;
        LastModifiedTime = modifiedDateTime;
        DeletedTime = deletedDateTime;
    }

    public static EmailSendingType Create(
            EmailTypeId id,
            string name,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Type cannot be null or empty.", nameof(name));
        }

        return new EmailSendingType(
            id,
            name,
            createdDateTime,
            modifiedDateTime,
            deletedDateTime);
    }
}
public sealed partial class EmailSendingType
{
    public IReadOnlyCollection<MailOutboxRecipient> MailOutboxRecipients { get; }
}
