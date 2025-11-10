using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard
{
    public class CreateCardCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? PhotoBase64 { get; set; }
    }
}
