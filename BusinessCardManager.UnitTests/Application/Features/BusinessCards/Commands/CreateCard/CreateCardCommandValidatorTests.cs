using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardManager.UnitTests.Application.Features.BusinessCards.Commands.CreateCard
{
    public class CreateCardCommandValidatorTests
    {
        private readonly CreateCardCommandValidator _validator;

        public CreateCardCommandValidatorTests()
        {
            _validator = new CreateCardCommandValidator();
        }

        [Fact]
        public void Validate_EmptyName_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateCardCommand { Name = "" };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Validate_InvalidEmail_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateCardCommand { Email = "notanemail" };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Validate_InvalidGender_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateCardCommand { Gender = "Invalid" };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Gender");
        }

        [Fact]
        public void Validate_PhotoTooLarge_ShouldHaveValidationError()
        {
            // Arrange
            // Create a base64 string that is roughly 1.5MB (1,500,000 * 4/3 > 1.4MB limit)
            var largeString = new string('A', 1_500_000);
            var command = new CreateCardCommand { PhotoBase64 = largeString };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PhotoBase64");
        }

        [Fact]
        public void Validate_ValidCommand_ShouldNotHaveErrors()
        {
            // Arrange
            var command = new CreateCardCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                Phone = "123456789",
                Gender = "Male",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "123 Test St"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
