using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record AuthId : Identification
    {
        public AuthId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
