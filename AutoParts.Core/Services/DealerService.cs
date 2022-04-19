namespace AutoParts.Core.Services
{
    using AutoParts.Core.Contract;
    using AutoParts.Infrastructure.Data;
    using System.Linq;

    public class DealerService : IDealerService
    {
        private readonly AutoPartsDbContext data;

        public DealerService(AutoPartsDbContext data)
            => this.data = data;

        public bool IsDealer(string userId)
            => this.data
                .Dealers
                .Any(d => d.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}