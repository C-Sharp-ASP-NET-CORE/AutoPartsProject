namespace AutoParts.Extensions
{
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<AutoPartsDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(AutoPartsDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name ="Engine"},
                new Category { Name ="Lights"},
                new Category { Name ="Wheels"},
                new Category { Name ="Interior"},
                new Category { Name ="Filters"},
                new Category { Name ="Other"}
            });

            data.SaveChanges();
        }
    }
}
