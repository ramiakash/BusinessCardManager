using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardManager.Infrastructure.Persistence
{
    public class BusinessCardRepository : IBusinessCardRepository
    {
        private readonly AppDbContext _context;

        public BusinessCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BusinessCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BusinessCards.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(BusinessCard card, CancellationToken cancellationToken)
        {
            await _context.BusinessCards.AddAsync(card, cancellationToken);
        }

        public void Delete(BusinessCard card)
        {
            _context.BusinessCards.Remove(card);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<BusinessCard>> GetAllAsync(
            string? name,
            string? email,
            string? phone,
            string? gender,
            DateTime? dob,
            CancellationToken cancellationToken)
        {
            IQueryable<BusinessCard> query = _context.BusinessCards.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(c => c.Email.Value.Contains(email));
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(c => c.Phone.Value.Contains(phone));
            }
            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(c => c.Gender == Gender.FromString(gender));
            }
            if (dob.HasValue)
            {
                query = query.Where(c => c.DateOfBirth == dob.Value.Date);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
