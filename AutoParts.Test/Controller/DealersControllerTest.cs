namespace AutoParts.Test.Controller
{
    using AutoParts.Controllers;
    using AutoParts.Core.Models.Dealers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
                    => MyController<DealersController>
            .Instance()
            .Calling(c => c.Become())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Dealer", "+35988888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string dealerName,
            string phoneNumber)
                => MyController<DealersController>
            .Instance(controller => controller
                .WithUser())
            .Calling(c => c.Become(new BecomeDealerFormModel
            {
                Name = dealerName,
                PhoneNumber = phoneNumber
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState();
    }
}
