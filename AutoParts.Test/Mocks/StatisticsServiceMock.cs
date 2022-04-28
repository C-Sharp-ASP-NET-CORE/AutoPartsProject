namespace AutoParts.Test.Mocks
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Statistics;
    using Moq;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s=>s.Total())
                    .Returns(new StatisticsServiceModel
                    { 
                        TotalParts =5,
                        TotalUsers=3
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
