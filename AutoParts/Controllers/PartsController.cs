
namespace AutoParts.Controllers
{
    using AutoParts.Data;
    using AutoParts.Data.Models;
    using AutoParts.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private readonly AutoPartsDbContext data;

        public PartsController(AutoPartsDbContext data)
             => this.data = data;

        public IActionResult Add()
            => View(new AddPartFormModel
            { 
                Categories = this.GetPartCategories()
            });

        [HttpPost]
        public IActionResult Add(AddPartFormModel part)
        {
            if (!this.data.Categories.Any(c => c.Id == part.CategoryId))
            {
                this.ModelState.AddModelError(nameof(part.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                part.Categories = this.GetPartCategories();
                return View(part);
            }

            var myPart = new Part
            {
                CategoryId = part.CategoryId,
                Manufacturer = part.Manufacturer,
                CarBrand = part.CarBrand,
                CarModel = part.CarModel,
                Price = part.Price,
                Description = part.Description,
                ImageUrl = part.ImageUrl,
                Year = part.Year,
                IsUsed = part.IsUsed
            };

            this.data.Parts.Add(myPart);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<PartCategoryViewModel> GetPartCategories()
           => this.data.Categories
                 .Select(c => new PartCategoryViewModel
                 {
                     Id = c.Id,
                     Name = c.Name
                 })
                 .ToList();
    }
}
