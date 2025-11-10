using BusinessCardManager.Domain.Exceptions;
using BusinessCardManager.Domain.ValueObjects;

namespace BusinessCardManager.UnitTests.Domain
{
    public class PhoneValueObjectTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        public void Create_WithInvalidPhone_ShouldThrowInvalidPhoneException(string invalidPhone)
        {
            // Arrange
            Action act = () => Phone.Create(invalidPhone);

            // Act & Assert
            Assert.Throws<InvalidPhoneException>(act);
        }

        [Fact]
        public void Create_WithValidPhone_ShouldCleanAndReturnPhoneObject()
        {
            // Arrange
            var validPhone = "(123) 456-7890";
            var expectedCleanPhone = "1234567890";

            // Act
            var phone = Phone.Create(validPhone);

            // Assert
            Assert.NotNull(phone);
            Assert.Equal(expectedCleanPhone, phone.Value);
        }
    }
}