namespace AutoParts.Controllers
{
    using AutoParts.Core.Constants;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Models;
    using AutoParts.Models.Home;
    using AutoParts.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly AutoPartsDbContext data;

        public HomeController(AutoPartsDbContext data)
             => this.data = data;

        public IActionResult Index()
        {
            ViewData[MessageConstant.SuccessMessage] = "Welcome";

            var totalParts = this.data.Parts.Count();

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

            return View(new IndexViewModel
                {
                TotalParts = totalParts,
                Parts = parts
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            =>View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
}
