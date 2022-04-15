namespace AutoParts.Infrastructure.Data
{
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Part>()
                .HasOne(p => p.Category)
                .WithMany(p=>p.Pars)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
