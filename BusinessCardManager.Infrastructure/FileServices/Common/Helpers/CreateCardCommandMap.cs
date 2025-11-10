using BusinessCardManager.Application.Features.BusinessCards.Commands.CreateCard;
using CsvHelper.Configuration;

namespace BusinessCardManager.Infrastructure.FileServices.Common.Helpers
{
    public sealed class CreateCardCommandMap : ClassMap<CreateCardCommand>
    {
        public CreateCardCommandMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.Gender).Index(1);
            Map(m => m.DateOfBirth).Index(2);
            Map(m => m.Email).Index(3);
            Map(m => m.Phone).Index(4);
            Map(m => m.Address).Index(5);
            Map(m => m.PhotoBase64).Index(6);
        }
    }
}
