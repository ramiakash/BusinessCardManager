using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using BusinessCardManager.Application.DTOs; // 1. Import your DTOs
using System.Xml.Serialization;

namespace BusinessCardManager.Infrastructure.FileServices.Parsers
{
    public class XmlFileParser : IFileParser
    {
        public string SupportedExtension => ".xml";
        public IEnumerable<CreateCardCommand> Parse(Stream fileStream)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<BusinessCardDto>), new XmlRootAttribute("BusinessCards"));

                var dtoList = (List<BusinessCardDto>?)serializer.Deserialize(fileStream);

                if (dtoList == null)
                {
                    return new List<CreateCardCommand>();
                }

                var commands = dtoList.Select(dto => new CreateCardCommand
                {
                    Name = dto.Name,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    PhotoBase64 = dto.PhotoBase64
                });

                return commands;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Failed to parse XML file.", ex);
            }
        }
    }
}