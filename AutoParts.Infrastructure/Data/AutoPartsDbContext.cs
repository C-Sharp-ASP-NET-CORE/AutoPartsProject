namespace AutoParts.Infrastructure.Data
{
    using AutoParts.Infrastructure.Data.Identity;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class AutoPartsDbContext : IdentityDbContext
    {
        public AutoPartsDbContext(DbContextOptions<AutoPartsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Part> Parts { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Contragent> Contragents { get; init; }
        public DbSet<Deal> Deals { get; init; }
        public DbSet<DealSubject> DealSubjects { get; init; }
        public DbSet<Rack> Racks { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contragent>()
                .HasIndex(c => c.CustomerNumber)
                .IsUnique();

            builder
                .Entity<Part>()
                .HasOne(p => p.Category)
                .WithMany(p=>p.Pars)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Deal>()
                .HasOne(d => d.Contragent)
                .WithMany(d => d.Deals)
                .HasForeignKey(d => d.ContragentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DealSubject>()
                .HasKey(x => new { x.DealId, x.PartId });

            base.OnModelCreating(builder);
        }
    }
}
