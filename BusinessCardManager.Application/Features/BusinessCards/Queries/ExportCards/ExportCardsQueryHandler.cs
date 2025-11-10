using AutoMapper;
using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.DTOs;
using BusinessCardManager.Domain.Interfaces;
using MediatR;

namespace BusinessCardManager.Application.Features.BusinessCards.Queries.ExportCards
{
    public class ExportCardsQueryHandler : IRequestHandler<ExportCardsQuery, FileDownloadDto>
    {
        private readonly IBusinessCardRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IFileExporter> _exporters;

        public ExportCardsQueryHandler(
            IBusinessCardRepository repository,
            IMapper mapper,
            IEnumerable<IFileExporter> exporters)
        {
            _repository = repository;
            _mapper = mapper;
            _exporters = exporters;
        }

        public async Task<FileDownloadDto> Handle(ExportCardsQuery request, CancellationToken cancellationToken)
        {
            var businessCards = await _repository.GetAllAsync(
                request.Name,
                request.Email,
                request.Phone,
                request.Gender,
                request.DateOfBirth,
                cancellationToken
            );

            var cardDtos = _mapper.Map<IEnumerable<BusinessCardDto>>(businessCards);

            IFileExporter exporter;
            if (request.Format == FileFormat.Csv)
            {
                exporter = _exporters.FirstOrDefault(e => e.ContentType == "text/csv")!;
            }
            else
            {
                exporter = _exporters.FirstOrDefault(e => e.ContentType == "application/xml")!;
            }

            if (exporter == null)
            {
                throw new InvalidOperationException($"No exporter found for format {request.Format}");
            }

            var fileData = exporter.Export(cardDtos);

            return new FileDownloadDto
            {
                Data = fileData,
                ContentType = exporter.ContentType,
                FileName = exporter.FileName
            };
        }
    }
}
