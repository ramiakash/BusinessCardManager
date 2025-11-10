using BusinessCardManager.Domain.Entities;
using BusinessCardManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardManager.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BusinessCard> BusinessCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var businessCardBuilder = modelBuilder.Entity<BusinessCard>();

            businessCardBuilder.HasKey(c => c.Id);

            businessCardBuilder.OwnsOne(c => c.Email, emailBuilder =>
            {
                emailBuilder.Property(e => e.Value)
                    .HasColumnName("Email") 
                    .IsRequired()
                    .HasMaxLength(255);
            });

            businessCardBuilder.OwnsOne(c => c.Phone, phoneBuilder =>
            {
                phoneBuilder.Property(p => p.Value)
                    .HasColumnName("Phone") 
                    .IsRequired()
                    .HasMaxLength(50);
            });

            businessCardBuilder.Property(c => c.Gender)
                .HasConversion(
                    g => g.Name,
                    s => Gender.FromString(s)
                )
                .IsRequired()
                .HasMaxLength(20);

            businessCardBuilder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            businessCardBuilder.Property(c => c.Address).HasMaxLength(500);

            businessCardBuilder.Property(c => c.PhotoBase64).HasColumnType("varchar(max)");
        }
    }
}
