using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record ChatId : Identification
    {
        public ChatId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
