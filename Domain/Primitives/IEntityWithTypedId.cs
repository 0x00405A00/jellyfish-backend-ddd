namespace Domain.Primitives
{
    public interface IEntityWithTypedId<TEntityId> where TEntityId : Identification
    {
        TEntityId Id { get; set; }

        bool Equals(Entity<TEntityId>? other);
        bool Equals(object? obj);
        int GetHashCode();
    }
}
