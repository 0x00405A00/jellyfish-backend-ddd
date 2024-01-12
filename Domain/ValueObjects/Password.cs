using Domain.Entities.Users;
using Domain.Primitives;
using System.Text;

namespace Domain.ValueObjects
{
    public class Password : IValueObjectNonBinary
    {

        public static string GeneratePassword()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();

            while (!IsValidPassword(passwordBuilder.ToString()))
            {
                passwordBuilder.Clear();

                // Add two uppercase letters
                passwordBuilder.Append(RandomUppercaseLetter());
                passwordBuilder.Append(RandomUppercaseLetter());

                // Add one special case letter
                passwordBuilder.Append(RandomSpecialCharacter());

                // Add two digits
                passwordBuilder.Append(RandomDigit());
                passwordBuilder.Append(RandomDigit());

                // Add three lowercase letters
                passwordBuilder.Append(RandomLowercaseLetter());
                passwordBuilder.Append(RandomLowercaseLetter());
                passwordBuilder.Append(RandomLowercaseLetter());

                // Fill the remaining characters with random characters
                for (int i = passwordBuilder.Length; i < 8; i++)
                {
                    char randomChar = (char)random.Next(33, 127); // ASCII printable characters
                    passwordBuilder.Append(randomChar);
                }
            }

            return passwordBuilder.ToString();
        }

        public static implicit operator string(Password password) => throw new NotImplementedException("need factory method and private value handling");

        public static bool IsValidPassword(string password)
        {
            foreach (var policy in User.PasswordPolicy)
            {
                if (!policy.Value.IsMatch(password))
                {
                    Console.WriteLine($"Password does not meet the criteria: {policy.Key}");
                    return false;
                }
            }

            return true;
        }

        public static char RandomUppercaseLetter()
        {
            Random random = new Random();
            return (char)random.Next('A', 'Z' + 1);
        }

        public static char RandomSpecialCharacter()
        {
            char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*' };
            Random random = new Random();
            return specialCharacters[random.Next(specialCharacters.Length)];
        }

        public static char RandomDigit()
        {
            Random random = new Random();
            return (char)random.Next('0', '9' + 1);
        }

        public static char RandomLowercaseLetter()
        {
            Random random = new Random();
            return (char)random.Next('a', 'z' + 1);
        }
    }
}
