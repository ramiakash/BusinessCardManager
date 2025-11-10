using BusinessCardManager.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardManager.Application.Features.BusinessCards.Queries.GetCardById
{
    public class GetCardByIdQuery : IRequest<BusinessCardDto?>
    {
        public Guid Id { get; set; }
    }
}
