namespace AutoParts.Test.Controller
{
    using AutoParts.Controllers;
    using AutoParts.Core.Models.Dealers;
    using AutoParts.Core.Models.Parts;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PartsControllerTest
    {
        [Theory]
        [InlineData(1, "Mercedes-Germany","Mercedes", "W211",1500, "testttt","DFRTE34","www",2005,true)]
        public void PostAddShouldBeSuccessfullForAuthorizedUsersAndReturnView(
           int categoryId,
           string manufacturer,
           string carBrand,
           string carModel,
           decimal price,
           string description,
           string serialNumber,
           string imageUrl,
           int year,
           bool isUsed)
               => MyController<PartsController>
           .Instance(controller => controller
               .WithUser())
           .Calling(c => c.Add(new PartFormModel
           {
               CategoryId = categoryId,
               Manufacturer = manufacturer,
               CarBrand = carBrand,
               CarModel = carModel,
               Price = price,
               Description = description,
               SerialNumber = serialNumber,
               ImageUrl = imageUrl,
               Year = year,
               IsUsed = isUsed
           }))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
               .RestrictingForHttpMethod(HttpMethod.Post)
               .RestrictingForAuthorizedRequests())
           .ValidModelState();
    }
}
