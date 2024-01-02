using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData("+491764567213", true)]
        [InlineData("01764567213", true)]
        [InlineData("12345", false)] // Ungültige Nummer, sollte fehlschlagen
        [InlineData("abc", false)]   // Ungültige Nummer, sollte fehlschlagen
        public void Parse_ValidPhoneAndInvalidPhoneNumbers(string phoneNumber, bool isValid)
        {
            // Act & Assert
            if (isValid)
            {
                var phone = PhoneNumber.Parse(phoneNumber);
                Assert.Equal(phone.PhoneNumb, phoneNumber);
            }
            else
            {
                var exception = Assert.Throws<NotValidPhoneNumberException>(() => PhoneNumber.Parse(phoneNumber));
                Assert.True(exception is NotValidPhoneNumberException);
            }
        }
    }
}
