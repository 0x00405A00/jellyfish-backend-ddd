using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record ChatInviteRequestId : Identification
    {
        public ChatInviteRequestId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
