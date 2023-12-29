namespace Domain.Primitives
{
    public interface IEntityWithTypedId<TEntityId> where TEntityId : Identification
    {
        TEntityId Id { get; init; }

        bool Equals(Entity<TEntityId>? other);
        bool Equals(object? obj);
        int GetHashCode();
    }
}
