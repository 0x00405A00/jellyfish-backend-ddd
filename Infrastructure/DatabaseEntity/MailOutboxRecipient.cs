using Infrastructure.Abstractions;

namespace Infrastructure.DatabaseEntity;

public partial class MailOutboxRecipient : DatabaseEntityModel
{
    public Guid MailUuid { get; set; } = Guid.Empty;

    public string Email { get; set; } = null!;

    public Guid EmailTypeUuid { get; set; } = Guid.Empty;

    public virtual EmailType EmailTypeUu { get; set; } = null!;

    public virtual MailOutbox MailUu { get; set; } = null!;
}
