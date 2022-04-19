namespace AutoParts.Infrastructure.Data
{
    using AutoParts.Infrastructure.Data.Identity;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class AutoPartsDbContext : IdentityDbContext<User>
    {
        public AutoPartsDbContext(DbContextOptions<AutoPartsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Part> Parts { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Dealer> Dealers { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Contragent>()
            //    .HasIndex(c => c.CustomerNumber)
            //    .IsUnique();
            builder
                .Entity<Part>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Parts)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Part>()
                .HasOne(p => p.Dealer)
                .WithMany(p=>p.Parts)
                .HasForeignKey(p => p.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<Deal>()
            //    .HasOne(d => d.Contragent)
            //    .WithMany(d => d.Deals)
            //    .HasForeignKey(d => d.ContragentId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<DealSubject>()
            //    .HasKey(x => new { x.DealId, x.PartId });

            builder
               .Entity<Dealer>()
               .HasOne<User>()
               .WithOne()
               .HasForeignKey<Dealer>(d => d.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
