using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record ChatId : Identification
    {
        public ChatId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
