namespace AutoParts.Controllers
{
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private readonly AutoPartsDbContext data;

        public PartsController(AutoPartsDbContext data)
             => this.data = data;

        [Authorize]
        public IActionResult Add()
            => View(new AddPartFormModel
            {
                Categories = this.GetPartCategories()
            });

        [HttpPost]
        [Authorize]
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
                SerialNumber = part.SerialNumber,
                ImageUrl = part.ImageUrl,
                Year = part.Year,
                IsUsed = part.IsUsed
            };

            this.data.Parts.Add(myPart);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllPartsQueryModel query)
        {

            var partsQuery = this.data.Parts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                partsQuery = partsQuery.Where(p => p.CarBrand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                partsQuery = partsQuery
                                    .Where(p =>
                                    p.Category.Name.ToLower().Contains(query.SearchTerm.ToLower())
                                    || (p.CarBrand + " " + p.CarModel).ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var totalParts = partsQuery.Count();

            partsQuery = query.Sorting switch
            {
                PartsSorting.Year => partsQuery.OrderByDescending(p => p.Year),
                PartsSorting.Price => partsQuery.OrderByDescending(p => p.Price),
                PartsSorting.BrandAndMode => partsQuery.OrderBy(p => p.CarBrand).ThenBy(p => p.CarModel),
                PartsSorting.DateCreated or _ => partsQuery.OrderByDescending(p => p.Id)
            };

            var parts = partsQuery
                                .Skip((query.CurrentPage - 1) * AllPartsQueryModel.PartsPerPage)
                                .Take(AllPartsQueryModel.PartsPerPage)
                                .Select(p => new PartListingViewModel
                                {
                                    Id = p.Id,
                                    Category = p.Category.Name,
                                    CarBrand = p.CarBrand,
                                    CarModel = p.CarModel,
                                    Price = p.Price,
                                    Year = p.Year,
                                    ImageUrl = p.ImageUrl
                                })
                                .ToList();

            var carBrands = this.data.Parts
                                        .Select(p => p.CarBrand)
                                        .Distinct()
                                        .OrderBy(br => br)
                                        .OrderBy(br => br)
                                        .ToList();

            query.TotalParts = totalParts;
            query.Brands = carBrands;
            query.Parts = parts;

            return View(query);
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
