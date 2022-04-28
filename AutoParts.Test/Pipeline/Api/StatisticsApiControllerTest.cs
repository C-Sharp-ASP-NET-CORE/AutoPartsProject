namespace AutoParts.Test.Controllers.Api
{
    using AutoParts.Controllers.Api;
    using AutoParts.Test.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        { 
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalParts);
            Assert.Equal(3, result.TotalUsers);

        }
    }
}
