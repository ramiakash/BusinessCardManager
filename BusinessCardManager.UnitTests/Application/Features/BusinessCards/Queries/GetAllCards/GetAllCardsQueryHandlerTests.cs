using Moq;
using AutoMapper;
using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Application.Features.BusinessCards.Queries.GetAllCards;
using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.ValueObjects;
using BusinessCardManager.Application.Common.Mappings;
using BusinessCardManager.Application.DTOs;
using Microsoft.Extensions.Logging.Abstractions;


namespace BusinessCardManager.UnitTests.Application.Features.BusinessCards.Queries.GetAllCards
{
    public class GetAllCardsQueryHandlerTests
    {
        private readonly Mock<IBusinessCardRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly GetAllCardsQueryHandler _handler;

        public GetAllCardsQueryHandlerTests()
        {
            _mockRepo = new Mock<IBusinessCardRepository>();

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile(new MappingProfile());

            var loggerFactory = new NullLoggerFactory();

            var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetAllCardsQueryHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task Handle_WithFilters_ShouldCallRepositoryAndReturnMappedDtos()
        {
            // Arrange
            var query = new GetAllCardsQuery { Name = "Test" };

            var fakeCards = new List<BusinessCard>
            {
                BusinessCard.Create(
                    "Test User",
                    Email.Create("test@example.com"),
                    Phone.Create("1234567"),
                    Gender.Male,
                    new DateTime(1990, 1, 1),
                    "123 St",
                    null)
            };

            _mockRepo.Setup(r => r.GetAllAsync(
                query.Name, query.Email, query.Phone, query.Gender, query.DateOfBirth,
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCards);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<BusinessCardDto>>(result);

            Assert.Single(result);

            var firstCard = result.First();
            Assert.Equal("Test User", firstCard.Name);
            Assert.Equal("test@example.com", firstCard.Email);
        }
    }
}