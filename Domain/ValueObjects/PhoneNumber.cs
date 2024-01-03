using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class PhoneNumber:IEquatable<PhoneNumber>
    {
        public static Regex PhoneNumberRegex = new Regex(@"^(?:\+49|0)(?:[1-9][0-9]{1,4})?[0-9]+$", RegexOptions.Compiled);
        public string PhoneNumb { get; private set; } 
        
        private PhoneNumber()
        {

        }

        private PhoneNumber(string value) 
        {
            PhoneNumb = value;
        }

        public static PhoneNumber Parse(string value)
        {
            if(String.IsNullOrEmpty(value))
            {
                throw new NotValidPhoneNumberException("given value is null or empty");
            }
            var result = PhoneNumberRegex.Match(value);
            if(!result.Success) {
                throw new NotValidPhoneNumberException(value);
            }

            return new PhoneNumber(value);
        }

        public override string ToString()
        {
            return PhoneNumb;
        }

        public static bool Contains(PhoneNumber phoneNumber, string value)
        {
            return phoneNumber.PhoneNumb.IndexOf(value) >= 0;
        }

        public static bool operator !=(PhoneNumber phonelLeft, PhoneNumber phoneRight)
        {
            if (ReferenceEquals(phonelLeft, null) && ReferenceEquals(phoneRight, null)) return true;
            if (ReferenceEquals(phonelLeft, null) || ReferenceEquals(phoneRight, null)) return false;

            return phonelLeft.Equals(phoneRight);
        }

        public static bool operator ==(PhoneNumber phoneLeft, PhoneNumber phoneRight)
        {
            return (phoneLeft != phoneRight);
        }

        public bool Equals(PhoneNumber? other)
        {
            if (ReferenceEquals(other, null)) return false;
            return this.PhoneNumb == other.PhoneNumb;
        }
    }
}
