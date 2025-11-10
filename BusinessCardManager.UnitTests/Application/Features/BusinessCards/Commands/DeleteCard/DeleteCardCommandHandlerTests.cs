using BusinessCardManager.Application.Common.Exceptions;
using BusinessCardManager.Application.Features.BusinessCards.Commands.DeleteCard;
using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardManager.UnitTests.Application.Features.BusinessCards.Commands.DeleteCard
{
    public class DeleteCardCommandHandlerTests
    {
        private readonly Mock<IBusinessCardRepository> _mockRepo;
        private readonly DeleteCardCommandHandler _handler;

        public DeleteCardCommandHandlerTests()
        {
            _mockRepo = new Mock<IBusinessCardRepository>();
            _handler = new DeleteCardCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ExistingCard_ShouldDeleteAndSaveChanges()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var command = new DeleteCardCommand { Id = cardId };
            var fakeCard = BusinessCard.Create(
                "Test",
                Email.Create("test@test.com"), 
                Phone.Create("1234567"), 
                Gender.Male,
                DateTime.Now,
                "123 St",
                null);

            _mockRepo.Setup(r => r.GetByIdAsync(cardId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(fakeCard);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepo.Verify(r => r.Delete(fakeCard), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingCard_ShouldThrowNotFoundException()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var command = new DeleteCardCommand { Id = cardId };

            _mockRepo.Setup(r => r.GetByIdAsync(cardId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((BusinessCard?)null);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
