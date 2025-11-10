using BusinessCardManager.Domain.Exceptions;
using BusinessCardManager.Domain.ValueObjects;

namespace BusinessCardManager.UnitTests.Domain
{
    public class EmailValueObjectTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("notanemail")]
        [InlineData("test@.com")]
        [InlineData("@example.com")]
        public void Create_WithInvalidEmail_ShouldThrowInvalidEmailException(string invalidEmail)
        {
            // Arrange
            Action act = () => Email.Create(invalidEmail);

            // Act & Assert
            Assert.Throws<InvalidEmailException>(act);
        }

        [Fact]
        public void Create_WithValidEmail_ShouldReturnEmailObject()
        {
            // Arrange
            var validEmail = "test@example.com";

            // Act
            var email = Email.Create(validEmail);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(validEmail, email.Value);
        }

        [Fact]
        public void Equality_WithTwoSameEmails_ShouldBeEqual()
        {
            // Arrange
            var email1 = Email.Create("test@example.com");
            var email2 = Email.Create("test@example.com");

            // Act & Assert
            Assert.True(email1 == email2);
            Assert.True(email1.Equals(email2));
            Assert.Equal(email1, email2);
        }
    }
}