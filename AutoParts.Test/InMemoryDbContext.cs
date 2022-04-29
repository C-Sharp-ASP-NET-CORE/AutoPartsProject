namespace AutoParts.Test
{
    using AutoParts.Infrastructure.Data;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    public class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<AutoPartsDbContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<AutoPartsDbContext>()
                .UseSqlServer(connection)
                .Options;

            using var context = new AutoPartsDbContext(dbContextOptions);

            context.Database.EnsureCreated();
        }

        public AutoPartsDbContext CreateContext() => new AutoPartsDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();
    }
}