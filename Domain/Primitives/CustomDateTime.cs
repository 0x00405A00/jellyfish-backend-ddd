namespace Domain.Primitives
{
    public class CustomDateTime(DateTime DateTime) : IComparable
    {
        public DateTime DateTime { get; } = DateTime;

        public static implicit operator string(CustomDateTime dt) => dt.DateTime.ToString();//for contains searching
        public static implicit operator CustomDateTime(DateTime dt) => new CustomDateTime(dt);//for between,greater/less than compare

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
        public static bool operator >=(CustomDateTime dtLeft, CustomDateTime dtRight)
        {
            return dtLeft > dtRight && dtLeft == dtRight;
        }
        public static bool operator <=(CustomDateTime dtLeft, CustomDateTime dtRight)
        {
            return !(dtLeft>=dtRight);
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

        public int CompareTo(object? obj)
        {
            if(obj == null) return 0;
            if(obj is CustomDateTime dt)
            {
                bool equal = this == dt;
                if(equal) return 0;

                bool greaterThan = this > dt;
                if(greaterThan) return 1;

                bool lessThan = this < dt;
                if(lessThan) return -1;
            }
            return 0;
        }
    }
}
