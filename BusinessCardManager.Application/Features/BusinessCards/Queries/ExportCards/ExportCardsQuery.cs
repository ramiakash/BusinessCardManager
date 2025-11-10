using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Queries.ExportCards
{
    public enum FileFormat { Csv, Xml }

    public class ExportCardsQuery : IRequest<FileDownloadDto>
    {
        public FileFormat Format { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
