namespace BusinessCardManager.Application.Features.BusinessCards.Queries.ExportCards
{
    public class FileDownloadDto
    {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
