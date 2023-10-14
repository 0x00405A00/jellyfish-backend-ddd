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
    public static class IdentificationExtension
    {
        public static TIdentification ToIdentification<TIdentification>(this Guid guid) 
            where TIdentification : Identification
        {

            var identification = (TIdentification)Activator.CreateInstance(typeof(TIdentification),new object[] { guid })!;
            
            return identification;
        }
        public static Guid ToGuid(this Identification identification)
        {
            return identification.Id;
        }
        public static bool IsValid(this Identification identification)
        {
            return identification.Id != Guid.Empty;
        }
    }
}
