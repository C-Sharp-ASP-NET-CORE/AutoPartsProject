namespace AutoParts.Core.Contract
{
    using AutoParts.Core.Models.Statistics;

    public interface IStatisticsService
    {
        StatisticsServiceModel Total();
    }
}