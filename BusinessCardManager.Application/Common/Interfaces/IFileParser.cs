using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardManager.Application.Common.Interfaces
{
    public interface IFileParser
    {
        string SupportedExtension { get; }
        IEnumerable<CreateCardCommand> Parse(Stream fileStream);
    }
}
