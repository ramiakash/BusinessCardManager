using Moq;
using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using BusinessCardManager.Domain.Entities;

namespace BusinessCardManager.UnitTests.Application.Features.BusinessCards.Commands.CreateCard
{
    public class CreateCardCommandHandlerTests
    {
        private readonly Mock<IBusinessCardRepository> _mockRepo;
        private readonly CreateCardCommandHandler _handler;

        public CreateCardCommandHandlerTests()
        {
            // Arrange (Shared for all tests)
            _mockRepo = new Mock<IBusinessCardRepository>();
            _handler = new CreateCardCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldAddCardToRepositoryAndSaveChanges()
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

            BusinessCard? capturedCard = null;
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<BusinessCard>(), It.IsAny<CancellationToken>()))
                     .Callback<BusinessCard, CancellationToken>((card, ct) => capturedCard = card);

            // Act
            var resultId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<BusinessCard>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _mockRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);

            Assert.NotEqual(Guid.Empty, resultId);
            Assert.NotNull(capturedCard);
            Assert.Equal(capturedCard.Id, resultId);

            Assert.Equal(command.Name, capturedCard.Name);
            Assert.Equal(command.Email, capturedCard.Email.Value);
        }
    }
}