using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record MailOutboxId : Identification
    {
        public MailOutboxId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
