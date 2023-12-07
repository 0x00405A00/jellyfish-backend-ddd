using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects
{
    public class PhoneNumberTests
    {
        [Fact]
        public void Parse_ValidPhoneNumber_ReturnsPhoneNumberObject()
        {
            // Arrange
            var validPhoneNumber = "(123)-456-7890";

            // Act
            var phoneNumber = PhoneNumber.Parse(validPhoneNumber);

            // Assert
            Assert.NotNull(phoneNumber);
            Assert.Equal(validPhoneNumber, phoneNumber.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123-456")]
        [InlineData("123-456-789")]
        [InlineData("(123)-456")]
        [InlineData("invalid")]
        public void Parse_InvalidPhoneNumber_ThrowsNotValidPhoneNumberException(string invalidPhoneNumber)
        {
            // Act and Assert
            var exception = Assert.Throws<NotValidPhoneNumberException>(() => PhoneNumber.Parse(invalidPhoneNumber));
            Assert.NotNull(exception);
        }
    }
}
