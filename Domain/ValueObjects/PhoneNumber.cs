using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class PhoneNumber
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
    }
}
