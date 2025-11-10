using BusinessCardManager.Domain.ValueObjects;

namespace BusinessCardManager.Domain.Entities
{
    public class BusinessCard
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Gender? Gender { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public string? Address { get; private set; }
        public string? PhotoBase64 { get; private set; }

        private BusinessCard() { }

        public static BusinessCard Create(
            string name,
            Email email,
            Phone phone,
            Gender? gender = null,
            DateTime? dateOfBirth = null,
            string? address = null,
            string? photoBase64 = null)
        {
            return new BusinessCard
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Phone = phone,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Address = address,
                PhotoBase64 = photoBase64
            };
        }
    }
}