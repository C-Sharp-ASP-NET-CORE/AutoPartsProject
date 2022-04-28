namespace AutoParts.Test.Mocks
{
    using AutoParts.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class DatabaseMock
    {
        public static AutoPartsDbContext Instance
        {
            get { 
            var dbContextOptions = new DbContextOptionsBuilder<AutoPartsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new AutoPartsDbContext(dbContextOptions);
            }
        }
    }
}
