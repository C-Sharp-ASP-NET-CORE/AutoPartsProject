namespace AutoParts.Controllers
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Home;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly AutoPartsDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(
            AutoPartsDbContext data,
            IStatisticsService statistics)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            //ViewData[MessageConstant.SuccessMessage] = "Welcome";

            var parts = this.data.Parts
                               .OrderByDescending(c => c.Id)
                               .Select(p => new PartIndexViewModel
                               {
                                   Id = p.Id,
                                   Category = p.Category.Name,
                                   CarBrand = p.CarBrand,
                                   CarModel = p.CarModel,
                                   Price = p.Price,
                                   Year = p.Year,
                                   ImageUrl = p.ImageUrl
                               })
                               .Take(3)
                               .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalParts = totalStatistics.TotalParts,
                TotalUsers = totalStatistics.TotalUsers,
                Parts = parts
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
