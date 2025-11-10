using AutoMapper;
using BusinessCardManager.Application.DTOs;
using BusinessCardManager.Domain.Interfaces;
using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Queries.GetAllCards
{
    public class GetAllCardsQueryHandler : IRequestHandler<GetAllCardsQuery, IEnumerable<BusinessCardDto>>
    {
        private readonly IBusinessCardRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCardsQueryHandler(IBusinessCardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BusinessCardDto>> Handle(GetAllCardsQuery request, CancellationToken cancellationToken)
        {
            var businessCards = await _repository.GetAllAsync(
                request.Name,
                request.Email,
                request.Phone,
                request.Gender,
                request.DateOfBirth,
                cancellationToken
            );

            var cardDtos = _mapper.Map<IEnumerable<BusinessCardDto>>(businessCards);

            return cardDtos;
        }
    }
}
