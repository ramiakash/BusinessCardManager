using BusinessCardManager.Domain.ValueObjects;

namespace BusinessCardManager.UnitTests.Domain
{
    public class GenderValueObjectTests
    {
        [Fact]
        public void FromString_WithValidName_ReturnsGender()
        {
            // Act
            var gender = Gender.FromString("Male");

            // Assert
            Assert.NotNull(gender);
            Assert.Equal(Gender.Male, gender);
        }

        [Fact]
        public void FromString_WithInvalidName_ThrowsException()
        {
            // Arrange
            Action act = () => Gender.FromString("InvalidGender");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}