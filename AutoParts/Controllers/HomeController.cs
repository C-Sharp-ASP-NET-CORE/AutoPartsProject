namespace AutoParts.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Home;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly AutoPartsDbContext data;
        private readonly IStatisticsService statistics;
        private readonly IConfigurationProvider mapper;

        public HomeController(
            AutoPartsDbContext data,
            IStatisticsService statistics,
            IMapper mapper)
        {
            this.data = data;
            this.statistics = statistics;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IActionResult Index()
        {
            //ViewData[MessageConstant.SuccessMessage] = "Welcome";

            var parts = this.data.Parts
                               .OrderByDescending(c => c.Id)
                               .ProjectTo<PartIndexViewModel>(this.mapper)
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
