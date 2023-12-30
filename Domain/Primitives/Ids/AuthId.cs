using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record AuthId : Identification
    {
        public AuthId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
