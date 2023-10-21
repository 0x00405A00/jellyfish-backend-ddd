using Infrastructure.Abstractions;

namespace Infrastructure.DatabaseEntity;

public partial class EmailType : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public string Type { get; set; } = null!;

    public virtual ICollection<MailOutboxRecipient> MailOutboxRecipients { get; set; } = new List<MailOutboxRecipient>();
}
