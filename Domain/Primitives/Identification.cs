namespace Domain.Primitives
{
    public record Identification 
    {
        public Guid Id { get; private set; }
        public Identification(Guid guid)
        {
            Id = guid;
        }
        public static Identification Create() => new Identification(Guid.NewGuid());
        public override string ToString()
        {
            return Id.ToString();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() *41;
        }
    }
}
