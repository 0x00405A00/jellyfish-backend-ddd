using Domain.Primitives;

namespace Domain.Entities.Auth
{
    public sealed class AuthId :Identification
    {
        public AuthId(Guid id) : base(id)
        {
                
        }
    }
}
