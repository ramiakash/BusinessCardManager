using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.DTOs;
using CsvHelper;
using System.Globalization;
using System.Text;

namespace BusinessCardManager.Infrastructure.FileServices.Exporters
{
    public class CsvFileExporter : IFileExporter
    {
        public string ContentType => "text/csv";
        public string FileName => $"BusinessCards_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";

        public byte[] Export(IEnumerable<BusinessCardDto> cards)
        {
            using var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, new UTF8Encoding(true)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(cards);
            }

            return memoryStream.ToArray();
        }
    }
}
