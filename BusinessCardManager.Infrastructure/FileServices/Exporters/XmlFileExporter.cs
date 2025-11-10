using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.DTOs;
using System.Text;
using System.Xml.Serialization;

namespace BusinessCardManager.Infrastructure.FileServices.Exporters
{
    public class XmlFileExporter : IFileExporter
    {
        public string ContentType => "application/xml";
        public string FileName => $"BusinessCards_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xml";

        public byte[] Export(IEnumerable<BusinessCardDto> cards)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCardDto>), new XmlRootAttribute("BusinessCards"));

            using var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                serializer.Serialize(writer, cards.ToList());
            }

            return memoryStream.ToArray();
        }
    }
}
