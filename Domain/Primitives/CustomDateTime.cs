namespace Domain.Primitives
{
    public class CustomDateTime(DateTime DateTime)
    {
        public DateTime DateTime { get; } = DateTime;

        public static implicit operator string(CustomDateTime dt) => dt.DateTime.ToString();
        public static bool operator !=(CustomDateTime dtLeft, CustomDateTime dtRight)
        {
            if (ReferenceEquals(dtLeft, null) && ReferenceEquals(dtRight, null)) return true;
            if (ReferenceEquals(dtLeft, null) || ReferenceEquals(dtRight, null)) return false;

            return dtLeft.Equals(dtRight);
        }

        public static bool operator ==(CustomDateTime dtLeft, CustomDateTime dtRight)
        {
            return (dtLeft != dtRight);
        }
        public static bool operator >(CustomDateTime dtLeft, CustomDateTime dtRight)
        {
            return (dtLeft.DateTime > dtRight.DateTime);
        }
        public static bool operator <(CustomDateTime dtLeft, CustomDateTime dtRight)
        {
            return !(dtLeft>dtRight);
        }

        public bool Equals(CustomDateTime? other)
        {
            if (ReferenceEquals(other, null)) return false;
            return this.DateTime == other.DateTime;
        }
        public static CustomDateTime Now() 
        { 
            return new CustomDateTime(DateTime.UtcNow);
        }
    }
}
