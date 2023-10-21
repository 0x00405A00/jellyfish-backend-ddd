using Domain.Exceptions;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class PhoneNumber:IEquatable<PhoneNumber>
    {
        public static Regex PhoneNumberRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", RegexOptions.Compiled);
        public string PhoneNumb { get; private set; } 
        private PhoneNumber(string value) {
            PhoneNumb = value;
        }
        public static PhoneNumber Parse(string value)
        {
            var result = PhoneNumberRegex.Match(value);
            if(!result.Success) {
                throw new NotValidPhoneNumberException(value);
            }

            return new PhoneNumber(result.Value);
        }
        public override string ToString()
        {
            return PhoneNumb;
        }

        public static bool operator !=(PhoneNumber phonelLeft, PhoneNumber phoneRight)
        {
            if (ReferenceEquals(phonelLeft, null) && ReferenceEquals(phoneRight, null)) return true;
            if (ReferenceEquals(phonelLeft, null)) return false;

            return phonelLeft.Equals(phoneRight);
        }
        public static bool operator ==(PhoneNumber phoneLeft, PhoneNumber phoneRight)
        {
            return !(phoneLeft != phoneRight);
        }

        public bool Equals(PhoneNumber? other)
        {
            if (ReferenceEquals(other, null)) return false;
            return this.PhoneNumb == other.PhoneNumb;
        }
    }
}
