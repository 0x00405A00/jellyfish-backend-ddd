using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record EmailTypeId : Identification
    {
        public EmailTypeId(Guid Uuid) : base(Uuid)
        {

        }
    }
}
