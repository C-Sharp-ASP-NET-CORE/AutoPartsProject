namespace AutoParts.Test.Pipeline
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class HomeControllerSystemTest : IClassFixture<WebApplicationFactory<Startup>>
    { 
        private readonly WebApplicationFactory<Startup> factory;

        public HomeControllerSystemTest(WebApplicationFactory<Startup> factory)
                    => this.factory = factory;

        public async void IndexShouldReturnCorrectResult()
        { 
            var client = this.factory.CreateClient();

            var result = await client.GetAsync("/");

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
