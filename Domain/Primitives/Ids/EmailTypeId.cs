using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record EmailTypeId : Identification
    {
        public EmailTypeId(Guid Uuid) : base(Uuid)
        {

        }
    }
}
