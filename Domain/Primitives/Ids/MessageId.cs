using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record MessageId : Identification
    {
        public MessageId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
