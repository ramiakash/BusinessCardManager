using AutoMapper;
using BusinessCardManager.Application.Common.Exceptions;
using BusinessCardManager.Application.Common.Mappings;
using BusinessCardManager.Application.Features.BusinessCards.Queries.GetCardById;
using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BusinessCardManager.UnitTests.Application.Features.BusinessCards.Queries.GetCardById
{
    public class GetCardByIdQueryHandlerTests
    {
        private readonly Mock<IBusinessCardRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly GetCardByIdQueryHandler _handler;

        public GetCardByIdQueryHandlerTests()
        {
            _mockRepo = new Mock<IBusinessCardRepository>();

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile(new MappingProfile());
            var loggerFactory = new NullLoggerFactory();
            var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetCardByIdQueryHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ExistingCard_ReturnsCardDto()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var command = new GetCardByIdQuery { Id = cardId };
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
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeCard.Name, result.Name);
            Assert.Equal(fakeCard.Email.Value, result.Email);
        }

        [Fact]
        public async Task Handle_NonExistingCard_ThrowsNotFoundException()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var command = new GetCardByIdQuery { Id = cardId };
            _mockRepo.Setup(r => r.GetByIdAsync(cardId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((BusinessCard?)null);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
