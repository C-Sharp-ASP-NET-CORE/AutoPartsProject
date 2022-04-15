using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AutoParts.Areas.Identity.IdentityHostingStartup))]
namespace AutoParts.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}