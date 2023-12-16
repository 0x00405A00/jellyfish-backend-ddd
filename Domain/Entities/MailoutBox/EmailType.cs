using Domain.Primitives;

namespace Domain.Entities.MailoutBox;

public sealed class EmailType : Entity<EmailTypeId>
{
    public string Type { get; private set; }
    
    private EmailType(string type)
    {
        Type = type;
    }

    public static EmailType Create(string type)
    {
        if (String.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        }

        return new EmailType(type);
    }
}
