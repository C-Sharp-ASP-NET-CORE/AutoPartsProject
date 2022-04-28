namespace AutoParts.Test.Services
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Services;
    using AutoParts.Infrastructure.Data.Models;
    using AutoParts.Test.Mocks;
    using Xunit;

    public class DealerServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIdIsDealer()
        {
            var dealerService = GetDealerService();

            var result = dealerService.IsDealer(UserId);

            Assert.True(result);
        }

        [Fact]
        public void IsDealerShouldReturnFalseWhenUserIdIsNotDealer()
        {
            var dealerService = GetDealerService();

            var result = dealerService.IsDealer("AnotherUserId");

            Assert.False(result);
        }

        private static IDealerService GetDealerService()
        {
            var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer { UserId = UserId });
            data.SaveChanges();

            return new DealerService(data);
        }
    }
}
