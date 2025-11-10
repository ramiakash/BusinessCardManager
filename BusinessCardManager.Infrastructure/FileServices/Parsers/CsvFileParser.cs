using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using BusinessCardManager.Infrastructure.FileServices.Common.Helpers;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace BusinessCardManager.Infrastructure.FileServices.Parsers
{
    public class CsvFileParser : IFileParser
    {
        public string SupportedExtension => ".csv";

        public IEnumerable<CreateCardCommand> Parse(Stream fileStream)
        {
            if (fileStream.Length == 0)
            {
                throw new InvalidDataException("CSV file is empty.");
            }

            List<CreateCardCommand> records;
            StreamReader reader = null;
            CsvReader csv = null;

            try
            {

                reader = new StreamReader(fileStream, leaveOpen: true);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null,
                    PrepareHeaderForMatch = args => args.Header.Trim()
                };

                csv = new CsvReader(reader, config);
                records = csv.GetRecords<CreateCardCommand>().ToList();
            }
            catch (HeaderValidationException)
            {

                try
                {
                    fileStream.Seek(0, SeekOrigin.Begin);

                    csv?.Dispose();
                    reader?.Dispose();

                    reader = new StreamReader(fileStream);
                    var configNoHeader = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                        MissingFieldFound = null
                    };

                    csv = new CsvReader(reader, configNoHeader);

                    csv.Context.RegisterClassMap<CreateCardCommandMap>();

                    records = csv.GetRecords<CreateCardCommand>().ToList();
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"File could not be parsed as a headered or headerless CSV. Details: {ex.Message}", ex);
                }
            }
            catch (CsvHelperException ex)
            {
                throw new InvalidDataException($"Failed to parse CSV file. Details: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("An unknown error occurred while processing the file.", ex);
            }
            finally
            {
                csv?.Dispose();
                reader?.Dispose();
            }


            if (!records.Any())
            {
                throw new InvalidDataException("CSV file is empty or contains only a header row.");
            }

            return records;
        }
    }
}