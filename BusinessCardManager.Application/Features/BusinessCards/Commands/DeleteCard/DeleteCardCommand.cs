using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Commands.DeleteCard
{
    public class DeleteCardCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
