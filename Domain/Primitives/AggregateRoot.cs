
namespace Domain.Primitives
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>
        where TEntityId : Identification
    {

        public AggregateRoot(TEntityId id) : base(id)
        {
            
        }
    }
}
