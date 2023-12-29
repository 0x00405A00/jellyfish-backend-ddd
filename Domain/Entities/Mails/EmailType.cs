using Domain.Primitives;
using Shared.ValueObjects.Ids;

namespace Domain.Entities.Mails;

public sealed class EmailType : Entity<EmailTypeId>
{
    public string Type { get; private set; }

    private EmailType(string type)
    {
        Type = type;
    }

    public static EmailType Create(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        }

        return new EmailType(type);
    }
}
