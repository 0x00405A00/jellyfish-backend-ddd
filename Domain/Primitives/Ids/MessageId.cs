using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record MessageId : Identification
    {
        public MessageId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
