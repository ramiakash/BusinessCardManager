using BusinessCardManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardManager.Application.Common.Interfaces
{
    public interface IFileExporter
    {
        byte[] Export(IEnumerable<BusinessCardDto> cards);
        string ContentType { get; }
        string FileName { get; }
    }
}
