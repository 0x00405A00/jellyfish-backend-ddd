using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record MessageOutboxId : Identification
    {
        public MessageOutboxId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
