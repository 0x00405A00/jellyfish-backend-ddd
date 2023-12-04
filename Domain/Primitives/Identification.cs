namespace Domain.Primitives
{
    public class Identification : IEquatable<Identification>
    {
        public Guid Id { get; private set; }
        public Identification() {
        
        }
        public Identification(Guid guid)
        {
            Id = guid;
        }
        public static bool operator !=(Identification? first, Identification? second)
        {
            if (first is null || second is null) return false;

            return first.Id != second.Id;
        }
        public static bool operator ==(Identification? first, Identification? second)
        {
            if (first is null || second is null) return false;
                
            return first.Id == second.Id;
        }
        public override string ToString()
        {
            return Id.ToString();
        }

        public bool Equals(Identification? other)
        {
            if (other is null) return false;

            return Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() *41;
        }
    }
}
