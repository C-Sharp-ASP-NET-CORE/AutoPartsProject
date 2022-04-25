﻿namespace AutoParts.Controllers
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

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

            const string latestPartsCacheKey = "LatestPartsCacheKey";

            var latestParts = this.cache.Get<List<LatestPartServiceModel>>(latestPartsCacheKey);

            if (latestParts==null)
            {
                latestParts = this.parts
                                    .Latest()
                                    .ToList();
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

            this.cache.Set(latestPartsCacheKey, latestParts, cacheOptions);

            return View(latestParts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
