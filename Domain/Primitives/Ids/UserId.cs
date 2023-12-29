using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record UserId : Identification
    {
        public UserId(Guid guid) : base(guid)
        {
        }
    }
}
