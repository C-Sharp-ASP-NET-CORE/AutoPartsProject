namespace AutoParts.Controllers
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static WebConstants.Cache;
    public class HomeController : BaseController
    {
        private readonly IPartService parts;
        private readonly IMemoryCache cache;

        public HomeController(
            IPartService parts, 
            IMemoryCache cache)
        {
            this.parts = parts;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            //ViewData[MessageConstant.SuccessMessage] = "Welcome";
            var latestParts = this.cache.Get<List<LatestPartServiceModel>>(LatestPartsCacheKey);

            if (latestParts==null)
            {
                latestParts = this.parts
                                    .Latest()
                                    .ToList();
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

            this.cache.Set(LatestPartsCacheKey, latestParts, cacheOptions);

            return View(latestParts);
        }

        public IActionResult Error() => View();

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //    => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
