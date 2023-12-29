using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record ChatRelationToUserId : Identification
    {
        public ChatRelationToUserId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
