namespace AutoParts.Controllers
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private readonly AutoPartsDbContext data;
        private readonly IPartService parts;

        public PartsController(
            AutoPartsDbContext data,
             IPartService parts)
        {
            this.data = data;
            this.parts = parts;
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new AddPartFormModel
            {
                Categories = this.GetPartCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPartFormModel part)
        {
            var dealerId = this.data
                                .Dealers
                                .Where(d => d.UserId == this.User.Id())
                                .Select(d => d.Id)
                                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

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
                IsUsed = part.IsUsed,
                DealerId = dealerId
            };

            this.data.Parts.Add(myPart);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllPartsQueryModel query)
        {
            var queryResult = this.parts.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPartsQueryModel.PartsPerPage);

            var partBrands = this.parts.AllPartBrands();

            query.Brands = partBrands;
            query.TotalParts = queryResult.TotalParts;
            query.Parts = queryResult.Parts;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myParts = this.parts.ByUser(this.User.Id());

            return View(myParts);
        }

        private IEnumerable<PartCategoryViewModel> GetPartCategories()
           => this.data.Categories
                 .Select(c => new PartCategoryViewModel
                 {
                     Id = c.Id,
                     Name = c.Name
                 })
                 .ToList();

        private bool UserIsDealer()
            => this.data
                    .Dealers
                    .Any(d => d.UserId == this.User.Id());  
    }
}
