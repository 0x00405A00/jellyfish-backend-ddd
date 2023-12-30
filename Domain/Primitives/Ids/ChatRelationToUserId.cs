using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record ChatRelationToUserId : Identification
    {
        public ChatRelationToUserId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
