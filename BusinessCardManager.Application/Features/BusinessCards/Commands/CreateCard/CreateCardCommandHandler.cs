using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Domain.ValueObjects;
using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Guid>
    {
        private readonly IBusinessCardRepository _repository;

        public CreateCardCommandHandler(IBusinessCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {

            var email = Email.Create(request.Email);
            var phone = Phone.Create(request.Phone);
            var gender = Gender.FromString(request.Gender);

            var businessCard = Domain.Entities.BusinessCard.Create(
                request.Name,
                email,
                phone,
                gender,
                request.DateOfBirth,
                request.Address,
                request.PhotoBase64
            );

            await _repository.AddAsync(businessCard, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return businessCard.Id;
        }
    }
}
