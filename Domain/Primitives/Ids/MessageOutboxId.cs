using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record MessageOutboxId : Identification
    {
        public MessageOutboxId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
