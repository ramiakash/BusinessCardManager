using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardManager.Application.Common.Interfaces
{
    public interface IQrCodeParser
    {
        CreateCardCommand Parse(Stream imageStream);
    }
}
