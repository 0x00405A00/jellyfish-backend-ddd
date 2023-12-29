using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record ChatInviteRequestId : Identification
    {
        public ChatInviteRequestId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
