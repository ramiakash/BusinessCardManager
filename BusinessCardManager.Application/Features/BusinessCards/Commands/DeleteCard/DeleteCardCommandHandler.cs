using BusinessCardManager.Application.Common.Exceptions;
using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.Interfaces;
using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Commands.DeleteCard
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand>
    {
        private readonly IBusinessCardRepository _repository;

        public DeleteCardCommandHandler(IBusinessCardRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            var businessCard = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (businessCard == null)
            {
                throw new NotFoundException(nameof(BusinessCard), request.Id);
            }

            _repository.Delete(businessCard);

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
