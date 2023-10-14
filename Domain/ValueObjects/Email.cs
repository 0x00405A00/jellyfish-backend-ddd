using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class Email
    {
        public static Regex EmailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.Compiled|RegexOptions.IgnoreCase);
        public string EmailValue { get; private set; } 
        private Email(string value) {
            EmailValue = value.ToLower();
        }
        public static Email Parse(string value)
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new NotValidEmailException(value);
            }
            var result = EmailRegex.Match(value);
            if(!result.Success) {
                throw new NotValidEmailException(value);
            }

            return new Email(result.Value);
        }
        public override string ToString()
        {
            return EmailValue;
        }
    }
}
