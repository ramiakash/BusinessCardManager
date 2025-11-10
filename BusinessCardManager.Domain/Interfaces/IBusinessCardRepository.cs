using BusinessCardManager.Domain.Entities;

namespace BusinessCardManager.Domain.Interfaces
{
    public interface IBusinessCardRepository
    {
        Task<BusinessCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<BusinessCard>> GetAllAsync(
            string? name,
            string? email,
            string? phone,
            string? gender,
            DateTime? dob,
            CancellationToken cancellationToken);

        Task AddAsync(BusinessCard card, CancellationToken cancellationToken);
        void Delete(BusinessCard card);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
