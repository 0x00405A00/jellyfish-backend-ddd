using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record MailOutboxId : Identification
    {
        public MailOutboxId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
