using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record RoleId : Identification
    {
        public RoleId(Guid guid) : base(guid)
        {

        }
    }
}
