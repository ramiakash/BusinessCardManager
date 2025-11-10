using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using BusinessCardManager.Application.Features.BusinessCards.Commands.DeleteCard;
using BusinessCardManager.Application.Features.BusinessCards.Queries.ExportCards;
using BusinessCardManager.Application.Features.BusinessCards.Queries.GetAllCards;
using BusinessCardManager.Application.Features.BusinessCards.Queries.GetCardById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEnumerable<IFileParser> _parsers;
        private readonly IQrCodeParser _qrParser;

        public BusinessCardsController(IMediator mediator, IEnumerable<IFileParser> parsers, IQrCodeParser qrParser)
        {
            _mediator = mediator;
            _parsers = parsers;
            _qrParser = qrParser;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCardsQuery query)
        {
            var cards = await _mediator.Send(query);
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var card = await _mediator.Send(new GetCardByIdQuery { Id = id });
            return Ok(card); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardCommand command)
        {
            var cardId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = cardId }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCardCommand { Id = id });
            return NoContent();
        }

        [HttpGet("export/{format}")]
        public async Task<IActionResult> Export(FileFormat format, [FromQuery] GetAllCardsQuery filters)
        {
            var query = new ExportCardsQuery
            {
                Format = format,
                Name = filters.Name,
                Email = filters.Email,
                Phone = filters.Phone,
                Gender = filters.Gender,
                DateOfBirth = filters.DateOfBirth
            };

            var file = await _mediator.Send(query);
            return File(file.Data, file.ContentType, file.FileName);
        }


        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension))
                return BadRequest("File has no extension.");

            var parser = _parsers.FirstOrDefault(p =>
                p.SupportedExtension.Equals(extension, StringComparison.OrdinalIgnoreCase));

            if (parser == null)
            {
                return BadRequest($"Unsupported file format: {extension}");
            }

            using var stream = file.OpenReadStream();
            var commands = parser.Parse(stream);

            return Ok(commands);
        }

        [HttpPost("import/qr")]
        public async Task<IActionResult> ImportQr(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = file.OpenReadStream();
            var command = _qrParser.Parse(stream);

            return Ok(command);
        }
    }
}
