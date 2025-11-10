using BusinessCardManager.Application.DTOs;
using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Queries.GetAllCards
{
    public class GetAllCardsQuery : IRequest<IEnumerable<BusinessCardDto>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
