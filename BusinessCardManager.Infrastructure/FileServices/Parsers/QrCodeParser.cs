using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using System.DrawingCore;
using System.Text.Json;
using ZXing;
using ZXing.Common;
using ZXing.ZKWeb;
namespace BusinessCardManager.Infrastructure.FileServices.Parsers
{
    public class QrCodeParser : IQrCodeParser
    {
        public CreateCardCommand Parse(Stream imageStream)
        {
            try
            {
                using var bitmap = (Bitmap)Image.FromStream(imageStream);

                var luminanceSource = new BitmapLuminanceSource(bitmap);
                var binarizer = new HybridBinarizer(luminanceSource);
                var binaryBitmap = new BinaryBitmap(binarizer);

                var reader = new MultiFormatReader();
                var result = reader.decode(binaryBitmap);

                if (result == null || string.IsNullOrEmpty(result.Text))
                {
                    throw new InvalidDataException("Could not read QR code from image.");
                }

                var command = JsonSerializer.Deserialize<CreateCardCommand>(result.Text,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (command == null)
                {
                    throw new InvalidDataException("QR code contained invalid data.");
                }

                command.PhotoBase64 = null;
                return command;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Failed to parse QR code.", ex);
            }
        }
    }
}
