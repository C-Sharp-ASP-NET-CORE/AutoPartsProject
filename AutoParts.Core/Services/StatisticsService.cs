namespace AutoParts.Core.Services
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Statistics;
    using AutoParts.Infrastructure.Data;
    using System.Linq;

    public class StatisticsService: IStatisticsService
    {
        private readonly AutoPartsDbContext data;

        public StatisticsService(AutoPartsDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalParts = this.data.Parts.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalParts = totalParts,
                TotalUsers = totalUsers
            };
        }
    }
}