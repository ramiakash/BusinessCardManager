using AutoMapper;
using BusinessCardManager.Application.Common.Exceptions;
using BusinessCardManager.Application.DTOs;
using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.Interfaces;
using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Queries.GetCardById
{
    public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, BusinessCardDto?>
    {
        private readonly IBusinessCardRepository _repository;
        private readonly IMapper _mapper;

        public GetCardByIdQueryHandler(IBusinessCardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BusinessCardDto?> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            var card = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (card == null)
            {
                throw new NotFoundException(nameof(BusinessCard), request.Id);
            }

            return _mapper.Map<BusinessCardDto>(card);
        }
    }
}
