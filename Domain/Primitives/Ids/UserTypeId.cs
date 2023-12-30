using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record UserTypeId : Identification
    {
        public UserTypeId(Guid guid) : base(guid)
        {

        }
    }
}
