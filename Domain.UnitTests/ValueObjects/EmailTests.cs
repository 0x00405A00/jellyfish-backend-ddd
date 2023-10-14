using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Parse_ValidEmail_ReturnsEmailObject()
        {
            // Arrange
            var validEmail = "test@example.com";

            // Act
            var email = Email.Parse(validEmail);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(validEmail, email.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid-email")]
        [InlineData("invalid.email@")]
        [InlineData("invalid.email@.com")]
        [InlineData("invalid.email@com.")]
        [InlineData("invalid.email@com")]
        public void Parse_InvalidEmail_ThrowsNotValidEmailException(string invalidEmail)
        {
            // Act and Assert
            var exception = Assert.Throws<NotValidEmailException>(() => Email.Parse(invalidEmail));
            Assert.Equal("Invalid email: " + invalidEmail, exception.Message);
        }
    }
}
